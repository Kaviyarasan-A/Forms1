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
        private string licenseKey; // License key passed from Form1
        private string validFrom;
        private string validTo;

        // Class-level variables to store the fetched sub-company details
        private string subCompanyName;
        private string connectionStringOnline;
        private string connectionStringOffline;
        private int subCompanyId;

        // Encryption key and IV
        private static readonly byte[] key = GenerateRandomKey();  // 16-byte key for AES-128
        private static readonly byte[] iv = GenerateRandomIV();    // 16-byte IV for AES

        // Constructor to initialize the form with responseData, subCompanyNameOrId, and licenseKey
        public Form2(string responseData, string subCompanyNameOrId, string licenseKey, string validFrom, string validTo)
        {
            InitializeComponent();

            // Log the value passed to the constructor
            this.validationResult = responseData;
            this.subCompanyNameOrId = subCompanyNameOrId;
            this.licenseKey = licenseKey;
            this.validFrom = validFrom;
            this.validTo = validTo;

            // Display the validation result
            lblValidationResult.Text = $"Validation result: {validationResult}";

            // Check if there are no sub-companies available
            if (string.IsNullOrEmpty(subCompanyNameOrId) || subCompanyNameOrId == "No sub-companies available.")
            {
                lblSubCompanyDetails.Text = "No sub-company available.";

                // Display the company (license key) details
                lblLicenseKey.Text = string.IsNullOrEmpty(licenseKey)
                    ? "License Key: Not available."
                    : $"License Key: {licenseKey}";

                lblCompanyName.Text = string.IsNullOrEmpty(licenseKey) ? "Company Name: Not Available" : "XYZ Corp";
                return;
            }

            // Load the sub-company details for the given subCompanyNameOrId
            LoadSubCompanyDetails(subCompanyNameOrId);
        }

        private async void LoadSubCompanyDetails(string subCompanyNameOrId)
        {
            try
            {
                lblSubCompanyDetails.Text = "Fetching sub-company details...";

                // Check if the subCompanyNameOrId is a number (ID)
                bool isId = int.TryParse(subCompanyNameOrId, out int subCompanyId);

                // API URL to fetch sub-company details by ID or name
                string url = isId
                    ? $"https://localhost:44395/api/LicenseValidation/GetSubCompanyDetailsById/subcompany/id/{subCompanyId}"
                    : $"https://localhost:44395/api/LicenseValidation/GetSubCompanyDetails/subcompany/{Uri.EscapeDataString(subCompanyNameOrId)}";

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    lblSubCompanyDetails.Text = $"Error fetching details: {response.StatusCode}. Response: {responseContent}";
                    return;
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                JObject parsedJson = JObject.Parse(responseBody);

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

        private async Task DownloadXmlAutomatically()
        {
            lblDownloadStatus.Text = "Downloading XML file... Please wait.";
            lblDownloadStatus.ForeColor = System.Drawing.Color.Black; // Initial color while downloading

            string xmlContent = ConvertToXml(subCompanyName, connectionStringOnline, connectionStringOffline, subCompanyId);
            string encryptedXml = EncryptString(xmlContent, key, iv);

            string directoryPath = @"C:\Users\aruch\source\repos\SubCompanyConfig";
            string fileName = $"{subCompanyName.Replace(" ", "_").Replace(":", "_").Replace("/", "_").Replace("\\", "_")}.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                await Task.Run(() =>
                {
                    System.IO.File.WriteAllText(filePath, encryptedXml);
                });

                // Update status with green color (success)
                lblDownloadStatus.Text = "Download successful! File saved.";
                lblDownloadStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                // Update status with red color (error)
                lblDownloadStatus.Text = $"Error downloading the file: {ex.Message}";
                lblDownloadStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private string ConvertToXml(string subCompanyName, string connectionStringOnline, string connectionStringOffline, int subCompanyId)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement("SubCompanyConfig");
            xmlDoc.AppendChild(rootElement);

            System.Xml.XmlElement nameElement = xmlDoc.CreateElement("SubCompanyName");
            nameElement.InnerText = subCompanyName;
            rootElement.AppendChild(nameElement);

            System.Xml.XmlElement onlineConnectionElement = xmlDoc.CreateElement("ConnectionStringOnline");
            onlineConnectionElement.InnerText = connectionStringOnline;
            rootElement.AppendChild(onlineConnectionElement);

            System.Xml.XmlElement offlineConnectionElement = xmlDoc.CreateElement("ConnectionStringOffline");
            offlineConnectionElement.InnerText = connectionStringOffline;
            rootElement.AppendChild(offlineConnectionElement);

            System.Xml.XmlElement idElement = xmlDoc.CreateElement("SubCompanyId");
            idElement.InnerText = subCompanyId.ToString();
            rootElement.AppendChild(idElement);

            return xmlDoc.OuterXml;
        }

        private static byte[] GenerateRandomKey() => Aes.Create().Key;
        private static byte[] GenerateRandomIV() => Aes.Create().IV;

        private string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
}
