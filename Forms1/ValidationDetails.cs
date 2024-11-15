﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Forms1
{
    public partial class ValidationDetails : Form
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

        // Encryption key and IV (with error handling for AES initialization)
        private static readonly byte[] key = GenerateRandomKey();  // 16-byte key for AES-128
        private static readonly byte[] iv = GenerateRandomIV();    // 16-byte IV for AES

        // Constructor to initialize the form with responseData, subCompanyNameOrId, and licenseKey
        public ValidationDetails(string responseData, string subCompanyNameOrId, string licenseKey, string validFrom, string validTo)
        {
            try
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

                // If license is valid locally, load from local XML
                var (isValid, message) = ValidateLicenseFromXml(licenseKey);
                if (isValid)
                {
                    // Load the sub-company details from local XML if valid
                    LoadSubCompanyDetailsFromXml(licenseKey);
                }
                else
                {
                    // If license is invalid locally, fetch details from the API
                    FetchSubCompanyDetailsFromApi(licenseKey);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing ValidationDetails: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Function to validate the license and fetch details from local XML
        private (bool isValid, string message) ValidateLicenseFromXml(string licenseKey)
        {
            string filePath = @"C:\Users\aruch\OneDrive\Documents\SubCompanyDetails.xml"; // Local XML file path
            try
            {
                // Load the XML document
                XDocument xmlDoc = XDocument.Load(filePath);

                // Search for the license key in the XML (with trimming)
                var license = xmlDoc.Descendants("License")
                    .FirstOrDefault(l => l.Element("LicenseKey")?.Value?.Trim() == licenseKey?.Trim());

                // If license is found
                if (license != null)
                {
                    bool isValid = bool.Parse(license.Element("IsValid")?.Value ?? "false");
                    if (isValid)
                    {
                        return (true, "License is valid.");
                    }
                    else
                    {
                        return (false, "The license is invalid.");
                    }
                }
                else
                {
                    return (false, "License key not found in the local file.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error reading the XML file: {ex.Message}");
            }
        }

        // Function to load sub-company details from the local XML file
        private void LoadSubCompanyDetailsFromXml(string licenseKey)
        {
            try
            {
                lblSubCompanyDetails.Text = "Fetching sub-company details from local XML...";

                // File path to local XML
                string filePath = @"C:\Users\aruch\OneDrive\Documents\SubCompanyDetails.xml";

                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    lblSubCompanyDetails.Text = "XML file not found at the specified path.";
                    return;
                }

                // Load the XML file
                XDocument xmlDoc = XDocument.Load(filePath);

                // Search for the License element based on the LicenseKey
                var licenseElement = xmlDoc.Descendants("License")
                    .FirstOrDefault(l => string.Equals(l.Element("LicenseKey")?.Value?.Trim(), licenseKey?.Trim(), StringComparison.OrdinalIgnoreCase));

                if (licenseElement != null)
                {
                    // Extract sub-company details from the License element
                    subCompanyName = licenseElement.Element("SubCompanyName")?.Value ?? "Not Available";
                    connectionStringOnline = licenseElement.Element("ConnectionStringOnline")?.Value ?? "Not Available";
                    connectionStringOffline = licenseElement.Element("ConnectionStringOffline")?.Value ?? "Not Available";
                    subCompanyId = int.Parse(licenseElement.Element("Id")?.Value ?? "0");

                    // Display the fetched details in the labels
                    lblSubCompanyDetails.Text = "Sub-company details loaded ";
                    lblSubCompanyName.Text = $"SubCompany: {subCompanyName}";
                    lblConnectionStringOnline.Text = $"Online Connection: {connectionStringOnline}";
                    lblConnectionStringOffline.Text = $"Offline Connection: {connectionStringOffline}";
                    lblSubCompanyId.Text = $"SubCompany ID: {subCompanyId}";
                    return;
                }
                else
                {
                    lblSubCompanyDetails.Text = $"License key not found in the local XML for LicenseKey: {licenseKey}";
                }
            }
            catch (FileNotFoundException fnfEx)
            {
                lblSubCompanyDetails.Text = $"File not found: {fnfEx.Message}";
            }
            catch (XmlException xmlEx)
            {
                lblSubCompanyDetails.Text = $"Error parsing XML: {xmlEx.Message}";
            }
            catch (Exception ex)
            {
                lblSubCompanyDetails.Text = $"Error loading sub-company details: {ex.Message}";
            }
        }

        // Function to fetch sub-company details from the API
        private async void FetchSubCompanyDetailsFromApi(string licenseKey)
        {
            try
            {
                lblSubCompanyDetails.Text = "Fetching sub-company details from API...";

                // Construct the API URL for license validation
                string url = $"https://localhost:44395/api/LicenseValidation/ValidateLicense/validate?licenceKey={Uri.EscapeDataString(licenseKey)}";

                // Make the HTTP request
                HttpResponseMessage response = await client.GetAsync(url);

                // Check if the request was successful
                if (!response.IsSuccessStatusCode)
                {
                    lblSubCompanyDetails.Text = $"Error: {response.StatusCode}";
                    return;
                }

                // Parse the response
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject parsedJson = JObject.Parse(responseBody);
                string messageFromApi = parsedJson["message"]?.ToString()?.Trim();

                // Process the API response
                if (messageFromApi.Equals("License is valid.", StringComparison.OrdinalIgnoreCase))
                {
                    var subCompaniesFromApi = parsedJson["subCompanies"]?.ToObject<List<SubCompany>>() ?? new List<SubCompany>();

                    if (subCompaniesFromApi.Any())
                    {
                        // Assuming we get at least one sub-company from the API response
                        var subCompany = subCompaniesFromApi.First(); // Get the first sub-company

                        // Ensure we have valid data before attempting to download the XML
                        if (string.IsNullOrEmpty(subCompany.subCompanyName) ||
                            string.IsNullOrEmpty(subCompany.connectionStringOnline) ||
                            string.IsNullOrEmpty(subCompany.connectionStringOffline) ||
                            subCompany.subCompanyId == 0)
                        {
                            lblSubCompanyDetails.Text = "Invalid sub-company details received from API.";
                            return;
                        }

                        // Set the sub-company details
                        subCompanyName = subCompany.subCompanyName;
                        connectionStringOnline = subCompany.connectionStringOnline;
                        connectionStringOffline = subCompany.connectionStringOffline;
                        subCompanyId = subCompany.subCompanyId;

                        // Display the fetched details in the labels
                        lblSubCompanyDetails.Text = "Sub-company details Saved Successfully.";
                        lblSubCompanyName.Text = $"SubCompany: {subCompanyName}";
                        lblConnectionStringOnline.Text = $"Online Connection: {connectionStringOnline}";
                        lblConnectionStringOffline.Text = $"Offline Connection: {connectionStringOffline}";
                        lblSubCompanyId.Text = $"SubCompany ID: {subCompanyId}";

                        // Trigger XML download after sub-company details are loaded
                        await DownloadXmlAutomatically(); // Initiate the XML download here
                    }
                    else
                    {
                        lblSubCompanyDetails.Text = "No sub-companies found in the API response.";
                    }
                }
                else
                {
                    lblSubCompanyDetails.Text = $"API error: {messageFromApi}";
                }
            }
            catch (Exception ex)
            {
                lblSubCompanyDetails.Text = $"Error fetching data from API: {ex.Message}";
            }
        }

        // Helper function to download the XML file automatically
        private async Task DownloadXmlAutomatically()
        {
            lblSubCompanyDetails.Text = "Downloading XML file... Please wait.";
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

                MessageBox.Show("Config file saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblSubCompanyDetails.Text = "File saved successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblSubCompanyDetails.Text = "Error downloading the file.";
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




            // AES Key and IV generation with error handling
            private static byte[] GenerateRandomKey()
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    return aesAlg.Key;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating key: {ex.Message}");
                return new byte[16];  // Fallback to a default key
            }
        }

        private static byte[] GenerateRandomIV()
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    return aesAlg.IV;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating IV: {ex.Message}");
                return new byte[16];  // Fallback to a default IV
            }
        }

        // SubCompany class used for deserialization from JSON response
        public class SubCompany
        {
            public int subCompanyId { get; set; }
            public string subCompanyName { get; set; }
            public string connectionStringOnline { get; set; }
            public string connectionStringOffline { get; set; }
        }
    }
}
