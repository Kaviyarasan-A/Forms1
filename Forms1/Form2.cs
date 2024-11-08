using System;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Forms1
{
    public partial class Form2 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private string validationResult;
        private string subCompanyNameOrId; // This can be either name or ID

        // Class-level variables to store the fetched sub-company details
        private string subCompanyName;
        private string connectionStringOnline;
        private string connectionStringOffline;
        private int subCompanyId;

        // Constructor to initialize the form with responseData and subCompanyNameOrId
        public Form2(string responseData, string subCompanyNameOrId)
        {
            InitializeComponent();

            // Log the value passed to the constructor
            Console.WriteLine($"Constructor called with subCompanyNameOrId: {subCompanyNameOrId}");

            this.validationResult = responseData;
            this.subCompanyNameOrId = subCompanyNameOrId;  // Initialize with subCompanyNameOrId

            // Display the validation result
            lblValidationResult.Text = $"Validation result: {validationResult}";

            // Load the sub-company details for the given subCompanyNameOrId
            LoadSubCompanyDetails(subCompanyNameOrId);
        }

        // Method to load and display sub-company details by subCompanyNameOrId (name or ID)
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
            }
            catch (Exception ex)
            {
                lblSubCompanyDetails.Text = $"Error fetching details: {ex.Message}";
            }
        }

        // Button click event to download the XML file
        private void btnDownloadXML_Click(object sender, EventArgs e)
        {
            // Convert the sub-company details to XML format
            string xmlContent = ConvertToXml(subCompanyName, connectionStringOnline, connectionStringOffline, subCompanyId);

            // Open SaveFileDialog to allow user to save the XML file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "xml";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // Write the XML content to the selected file
                System.IO.File.WriteAllText(filePath, xmlContent);

                MessageBox.Show("Config file saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
