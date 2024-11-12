using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace Forms1
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        // Method to validate the license against a local XML file
        private (bool isValid, List<SubCompany> subCompanies, string message) ValidateLicenseFromXml(string licenseKey)
        {
            string filePath = @"C:\Users\aruch\OneDrive\Documents\Filetocheck.xml"; // Local XML file path
            List<SubCompany> subCompanies = new List<SubCompany>();
            string message = "";

            try
            {
                if (System.IO.File.Exists(filePath))  // Check if the file exists
                {
                    XDocument xmlDoc = XDocument.Load(filePath);  // Load XML document
                    var license = xmlDoc.Descendants("License")
                        .Where(l => l.Element("LicenseKey")?.Value == licenseKey)
                        .FirstOrDefault();

                    if (license != null)
                    {
                        bool isValid = bool.Parse(license.Element("IsValid")?.Value ?? "false");
                        if (isValid)
                        {
                            // License is valid, parse sub-companies
                            subCompanies = license.Descendants("SubCompany")
                                .Select(sc => new SubCompany
                                {
                                    subCompanyId = int.Parse(sc.Element("Id")?.Value ?? "0"),
                                    subCompanyName = sc.Element("Name")?.Value
                                })
                                .ToList();

                            message = "License is valid.";
                            return (true, subCompanies, message);
                        }
                        else
                        {
                            message = "The license is invalid.";
                        }
                    }
                    else
                    {
                        message = "License key not found in the local file.";
                    }
                }
                else
                {
                    message = $"The XML file at {filePath} does not exist. Please check the file path.";
                }
            }
            catch (Exception ex)
            {
                message = $"Error reading the XML file: {ex.Message}";
            }

            return (false, subCompanies, message);
        }

        // Button click event to trigger the license validation
        private async void button1_Click(object sender, EventArgs e)
        {
            string licenseKey = textBox2.Text.Trim();  // This is the input license key from the text box.

            // Check if the license key is empty or null
            if (string.IsNullOrEmpty(licenseKey))
            {
                lblResult.Text = "Please enter a valid license key.";
                return;
            }

            try
            {
                lblResult.Text = "Validating...";

                // Check the license in the local XML file first
                var (isValid, subCompanies, message) = ValidateLicenseFromXml(licenseKey);

                // Log validation to a file and database
                LogValidation(licenseKey, isValid, message);

                if (isValid)
                {
                    // If no sub-companies exist, open Form2 and show message
                    if (subCompanies.Count == 0)
                    {
                        // Redirect to Form2 if there are no sub-companies
                        Form2 noSubCompanyForm = new Form2("License is valid, but no sub-companies are available.", "", licenseKey, "", "");
                        noSubCompanyForm.Show();
                        lblResult.Text = "License is valid, but no sub-companies are available.";
                        return;  // Exit early as the redirect is handled
                    }

                    // If sub-companies exist, populate the ComboBox
                    PopulateSubCompanies(subCompanies);
                    lblResult.Text = "License validated successfully.";
                }
                else
                {
                    lblResult.Text = message; // Show the message from XML validation
                    comboBoxSubCompanies.Items.Clear();

                    // If the license key is not found in the local file, prompt the user to connect to the internet
                    if (message == "License key not found in the local file.")
                    {
                        lblResult.Text = "License key not found in the local file. Please connect to the internet.";

                        // Now, try to validate the license online
                        string url = $"https://localhost:44395/api/LicenseValidation/ValidateLicense/validate?licenceKey={Uri.EscapeDataString(licenseKey)}";
                        HttpResponseMessage response = await client.GetAsync(url);

                        if (!response.IsSuccessStatusCode)
                        {
                            string errorResponseBody = await response.Content.ReadAsStringAsync();
                            lblResult.Text = errorResponseBody;
                            comboBoxSubCompanies.Items.Clear();
                            return;  // Exit early as the error has been handled
                        }

                        string responseBody = await response.Content.ReadAsStringAsync();
                        JObject parsedJson = JObject.Parse(responseBody);
                        string messageFromApi = parsedJson["message"]?.ToString()?.Trim();

                        // Process the response from the API as before
                        if (!string.IsNullOrEmpty(messageFromApi))
                        {
                            if (messageFromApi.Equals("License is valid.", StringComparison.OrdinalIgnoreCase))
                            {
                                var subCompaniesFromApi = parsedJson["subCompanies"]?.ToObject<List<SubCompany>>() ?? new List<SubCompany>();

                                // If no sub-companies exist from the API, open Form2
                                if (subCompaniesFromApi.Count == 0)
                                {
                                    Form2 noSubCompanyForm = new Form2("License is valid, but no sub-companies are available.", "", licenseKey, "", "");
                                    noSubCompanyForm.Show();
                                    lblResult.Text = "License is valid, but no sub-companies are available.";
                                    return;  // Exit early as the redirect is handled
                                }

                                PopulateSubCompanies(subCompaniesFromApi);
                                lblResult.Text = "License validated successfully.";
                            }
                            else
                            {
                                lblResult.Text = messageFromApi;  // Handle different messages from the API
                                comboBoxSubCompanies.Items.Clear();
                            }
                        }
                        else
                        {
                            lblResult.Text = "The server response is missing a message. Please contact support.";
                            comboBoxSubCompanies.Items.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = "An error occurred. Please try again later.";
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        // Method to populate the ComboBox with SubCompanies
        private void PopulateSubCompanies(List<SubCompany> subCompanies)
        {
            // Clear the ComboBox before adding new items
            comboBoxSubCompanies.Items.Clear();
            comboBoxSubCompanies.Items.Add("Select a sub-company");  // Optional: Add the default "Select a sub-company"

            // Add each SubCompany to the ComboBox
            foreach (var subCompany in subCompanies)
            {
                comboBoxSubCompanies.Items.Add(new ComboBoxItem
                {
                    Text = subCompany.subCompanyName,
                    Value = subCompany.subCompanyId
                });
            }

            // Set the default selected item as "Select a sub-company"
            comboBoxSubCompanies.SelectedIndex = 0;
        }

        // ComboBox selection changed event handler
        private void comboBoxSubCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the selected value is not the default "Select a sub-company"
            if (comboBoxSubCompanies.SelectedIndex > 0)
            {
                // Get the selected SubCompanyItem
                if (comboBoxSubCompanies.SelectedItem is ComboBoxItem selectedItem)
                {
                    int selectedId = selectedItem.Value;  // Get the selected SubCompany ID
                    string validationResponse = lblResult.Text;  // Use the actual validation result message

                    // Now create Form2 and pass both the validation result and the SubCompanyId
                    Form2 subCompanyDetailsForm = new Form2(validationResponse, selectedId.ToString(), textBox2.Text.Trim(), "", "");
                    subCompanyDetailsForm.Show();  // Show Form2 with the validation result and sub-company details
                }
            }
            else
            {
                // Handle the case where "Select a sub-company" is still selected
                lblResult.Text = "Please select a valid sub-company.";
            }
        }

        // Method to log validation information to a text file
        private void LogValidation(string licenseKey, bool isValid, string message)
        {
            string logFilePath = @"C:\Users\aruch\OneDrive\Documents\license_validation_log.txt";

            string logEntry = $"{DateTime.Now}: License Key: {licenseKey}, Valid: {isValid}, Message: {message}";
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine); // Append log to the file
        }

        // Method to store validation logs in the database
        private void LogValidationToDatabase(string licenseKey, bool isValid, string message)
        {
            string connectionString = "Server=SANDIYAR\\SQLEXPRESS;Database=Liscence;User Id=sa;Password=Kavi@123;"; // Replace with your database connection string
            string query = "INSERT INTO ValidationLogs (LicenseKey, ValidationResult, ValidationMessage, Timestamp) VALUES (@LicenseKey, @ValidationResult, @ValidationMessage, @Timestamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseKey", licenseKey);
                command.Parameters.AddWithValue("@ValidationResult", isValid ? "valid" : "invalid");
                command.Parameters.AddWithValue("@ValidationMessage", message);
                command.Parameters.AddWithValue("@Timestamp", DateTime.Now);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    // SubCompany model
    public class SubCompany
    {
        public int subCompanyId { get; set; }
        public string subCompanyName { get; set; }
    }

    // Helper class for ComboBox items
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
