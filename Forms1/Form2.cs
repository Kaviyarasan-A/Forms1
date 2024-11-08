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

                // Log the status code and response if not successful
                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    lblSubCompanyDetails.Text = $"Error fetching details: {response.StatusCode}. Response: {responseContent}";
                    return;
                }

                var responseBody = await response.Content.ReadAsStringAsync();

                // Log the API response for debugging purposes
                Console.WriteLine($"API Response: {responseBody}");

                // Parse the JSON response
                JObject parsedJson = JObject.Parse(responseBody);

                // Extract sub-company details including subCompanyId and subCompanyName
                string subCompanyNameResponse = parsedJson["subCompanyName"]?.ToString();
                string connectionStringOnline = parsedJson["connectionStringOnline"]?.ToString();
                string connectionStringOffline = parsedJson["connectionStringOffline"]?.ToString();
                int fetchedSubCompanyId = (int)(parsedJson["subCompanyId"] ?? 0);  // Default to 0 if not found

                // Log the extracted values for debugging
                Console.WriteLine($"subCompanyNameResponse: {subCompanyNameResponse}");
                Console.WriteLine($"connectionStringOnline: {connectionStringOnline}");
                Console.WriteLine($"connectionStringOffline: {connectionStringOffline}");
                Console.WriteLine($"fetchedSubCompanyId: {fetchedSubCompanyId}");

                // Display the fetched details in the labels
                lblSubCompanyDetails.Text = "Sub-company details loaded successfully.";
                lblSubCompanyName.Text = $"SubCompany: {subCompanyNameResponse}";
                lblConnectionStringOnline.Text = $"Online Connection: {connectionStringOnline}";
                lblConnectionStringOffline.Text = $"Offline Connection: {connectionStringOffline}";
                lblSubCompanyId.Text = $"SubCompany ID: {fetchedSubCompanyId}";  // Display the subCompanyId
            }
            catch (Exception ex)
            {
                // Log the exception details
                lblSubCompanyDetails.Text = $"Error fetching details: {ex.Message}";  // Error handling
            }
        }

        // Button click event to download the XML file
        private void btnDownloadXML_Click(object sender, EventArgs e)
        {
            // Convert the response data to XML format
            string xmlContent = ConvertToXml(validationResult);

            // Open SaveFileDialog to allow user to save the XML file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "xml";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // Write the XML content to the selected file
                System.IO.File.WriteAllText(filePath, xmlContent);

                MessageBox.Show("File saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Method to convert the response into XML format
        private string ConvertToXml(string responseData)
        {
            // Create a new XmlDocument
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

            // Optionally, wrap the response in an XML root element
            System.Xml.XmlElement rootElement = xmlDoc.CreateElement("LicenseValidationResult");
            xmlDoc.AppendChild(rootElement);

            // Add the response as a child element
            System.Xml.XmlElement responseElement = xmlDoc.CreateElement("Result");
            responseElement.InnerText = responseData;
            rootElement.AppendChild(responseElement);

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
