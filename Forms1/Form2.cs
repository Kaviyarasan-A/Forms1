using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Forms1
{
    public partial class Form2 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private string validationResult;
        private string subCompanyNameOrId; // This can be either name or ID
        private string licenseKey; // New variable to hold the license key

        // Class-level variables to store the fetched sub-company details
        private string subCompanyName;
        private string connectionStringOnline;
        private string connectionStringOffline;
        private int subCompanyId;

        // Encryption key and IV
        private static readonly byte[] key = GenerateRandomKey();  // 16-byte key for AES-128
        private static readonly byte[] iv = GenerateRandomIV();    // 16-byte IV for AES

        // Constructor to initialize the form with responseData, subCompanyNameOrId, and licenseKey
        public Form2(string responseData, string subCompanyNameOrId, string licenseKey)
        {
            InitializeComponent();

            // Set the form size to make it larger (adjust width and height as needed)
            this.Size = new System.Drawing.Size(800, 600); // Example: Set form size to 800x600

            // Log the value passed to the constructor
            Console.WriteLine($"Constructor called with subCompanyNameOrId: {subCompanyNameOrId}");

            this.validationResult = responseData;
            this.subCompanyNameOrId = subCompanyNameOrId;  // Initialize with subCompanyNameOrId
            this.licenseKey = licenseKey; // Initialize with license key

            // Display the validation result
            lblValidationResult.Text = $"Validation result: {validationResult}";

            // Check if there are no sub-companies available
            if (string.IsNullOrEmpty(subCompanyNameOrId) || subCompanyNameOrId == "No sub-companies available.")
            {
                // Display the message that no sub-company is available
                lblSubCompanyDetails.Text = "No sub-company available.";

                lblSubCompanyName.Text = string.Empty;
                lblConnectionStringOnline.Text = string.Empty;
                lblConnectionStringOffline.Text = string.Empty;
                lblSubCompanyId.Text = string.Empty;

                // Display the license key details
                lblLicenseKey.Text = string.IsNullOrEmpty(licenseKey)
                    ? "License Key: Not available."
                    : $"License Key: {licenseKey}";

                return;
            }

            // Load the sub-company details for the given subCompanyNameOrId
            LoadSubCompanyDetails(subCompanyNameOrId);
        }

        private async void LoadSubCompanyDetails(string subCompanyNameOrId)
        {
            try
            {
                lblSubCompanyDetails.Text = "Fetching sub-company details...";  // Update label text while loading

                // Check if the subCompanyNameOrId is a number (ID)
                bool isId = int.TryParse(subCompanyNameOrId, out int subCompanyId);

                // API URL to fetch sub-company details by ID or name
                string url;
                if (isId)
                {
                    // If subCompanyNameOrId is an ID, fetch details using the ID
                    url = $"https://localhost:44395/api/LicenseValidation/GetSubCompanyDetailsById/subcompany/id/{subCompanyId}";
                }
                else
                {
                    // If subCompanyNameOrId is a name, fetch details using the name
                    url = $"https://localhost:44395/api/LicenseValidation/GetSubCompanyDetails/subcompany/{Uri.EscapeDataString(subCompanyNameOrId)}";
                }

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    lblSubCompanyDetails.Text = $"Error fetching details: {response.StatusCode}. Response: {responseContent}";
                    return;
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                JObject parsedJson = JObject.Parse(responseBody);

                // Store the sub-company details
                subCompanyName = parsedJson["subCompanyName"]?.ToString();
                connectionStringOnline = parsedJson["connectionStringOnline"]?.ToString();
                connectionStringOffline = parsedJson["connectionStringOffline"]?.ToString();
                subCompanyId = (int)(parsedJson["subCompanyId"] ?? 0);

                // Display the fetched details in the labels
                lblSubCompanyDetails.Text = "Sub-company details loaded successfully.";
                lblSubCompanyName.Text = $"SubCompany: {subCompanyName}";
                lblConnectionStringOnline.Text = $"Online Connection: {connectionStringOnline}";
                lblConnectionStringOffline.Text = $"Offline Connection: {connectionStringOffline}";
                lblSubCompanyId.Text = $"SubCompany ID: {subCompanyId}";

                // Automatically download the XML file after loading the details
                await DownloadXmlAutomatically();
            }
            catch (Exception ex)
            {
                lblSubCompanyDetails.Text = $"Error fetching details: {ex.Message}";
            }
        }

        // Method to automatically download the XML file after sub-company details are fetched
        private async Task DownloadXmlAutomatically()
        {
            // Show the "Downloading..." message
            lblSubCompanyDetails.Text = "Downloading XML file... Please wait.";
            lblDownloadStatus.Text = "Download in progress...";
            progressBar.Style = ProgressBarStyle.Marquee;  // Show moving animation

            // Convert the sub-company details to XML format
            string xmlContent = ConvertToXml(subCompanyName, connectionStringOnline, connectionStringOffline, subCompanyId);

            // Encrypt the XML content before saving
            string encryptedXml = EncryptString(xmlContent, key, iv);

            // Define the directory to save the XML file
            string directoryPath = @"C:\Users\aruch\source\repos\SubCompanyConfig"; // Update with a valid directory

            // Use the sub-company name for the filename, and ensure it's valid for a file name
            string fileName = $"{subCompanyName.Replace(" ", "_").Replace(":", "_").Replace("/", "_").Replace("\\", "_")}.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                // Ensure the directory exists, if not, create it
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Simulate the file download (saving process)
                await Task.Run(() =>
                {
                    // Write the encrypted XML content to the file
                    System.IO.File.WriteAllText(filePath, encryptedXml);
                });

                // After the download is complete, show the "File saved successfully" message
                lblDownloadStatus.Invoke(new Action(() =>
                {
                    MessageBox.Show("Config file saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblSubCompanyDetails.Text = "File saved successfully.";
                    lblDownloadStatus.Text = "File saved!";
                    lblDownloadStatus.ForeColor = System.Drawing.Color.Green;
                    progressBar.Style = ProgressBarStyle.Blocks;  // Stop animation
                    progressBar.Value = 100;  // Fill progress bar to 100%
                }));
            }
            catch (Exception ex)
            {
                // In case of error, display an error message
                lblDownloadStatus.Invoke(new Action(() =>
                {
                    MessageBox.Show($"Error saving the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblSubCompanyDetails.Text = "Error downloading the file.";
                    lblDownloadStatus.Text = "Download failed!";
                }));
            }
        }

        // Method to convert the response into XML format
        private string ConvertToXml(string subCompanyName, string connectionStringOnline, string connectionStringOffline, int subCompanyId)
        {
            // Create a new XmlDocument
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

            // Create the root element for the config file
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement("SubCompanyConfig");
            xmlDoc.AppendChild(rootElement);

            // Add sub-company name
            System.Xml.XmlElement nameElement = xmlDoc.CreateElement("SubCompanyName");
            nameElement.InnerText = subCompanyName;
            rootElement.AppendChild(nameElement);

            // Add online connection string
            System.Xml.XmlElement onlineConnectionElement = xmlDoc.CreateElement("ConnectionStringOnline");
            onlineConnectionElement.InnerText = connectionStringOnline;
            rootElement.AppendChild(onlineConnectionElement);

            // Add offline connection string
            System.Xml.XmlElement offlineConnectionElement = xmlDoc.CreateElement("ConnectionStringOffline");
            offlineConnectionElement.InnerText = connectionStringOffline;
            rootElement.AppendChild(offlineConnectionElement);

            // Add sub-company ID
            System.Xml.XmlElement idElement = xmlDoc.CreateElement("SubCompanyId");
            idElement.InnerText = subCompanyId.ToString();
            rootElement.AppendChild(idElement);

            // Convert XmlDocument to string
            using (System.IO.StringWriter stringWriter = new System.IO.StringWriter())
            using (System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(stringWriter))
            {
                xmlWriter.Formatting = System.Xml.Formatting.Indented;
                xmlDoc.WriteTo(xmlWriter);
                return stringWriter.ToString();
            }
        }

        // AES Encryption Helper Methods

        // Generate random 16-byte key for AES
        public static byte[] GenerateRandomKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[16]; // AES-128 requires 16-byte keys
                rng.GetBytes(key);
                return key;
            }
        }

        // Generate random 16-byte IV for AES
        public static byte[] GenerateRandomIV()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] iv = new byte[16]; // AES-128 requires 16-byte IV
                rng.GetBytes(iv);
                return iv;
            }
        }

        // Encrypt the string using AES
        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
}
