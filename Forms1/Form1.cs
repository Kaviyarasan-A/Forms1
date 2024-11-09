using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Forms1
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        // Button click event to trigger the license validation
        private async void button1_Click(object sender, EventArgs e)
        {
            string licenseKey = textBox2.Text.Trim();

            // Check if the license key is empty or null
            if (string.IsNullOrEmpty(licenseKey))
            {
                lblResult.Text = "Please enter a valid license key.";
                return;
            }

            try
            {
                lblResult.Text = "Validating...";

                // Properly build the URL with URL encoding for the query parameter
                string url = $"https://localhost:44395/api/LicenseValidation/ValidateLicense/validate?licenceKey={Uri.EscapeDataString(licenseKey)}";

                // Make the HTTP request and get the full response
                HttpResponseMessage response = await client.GetAsync(url);

                // Check if the status code indicates a failure (e.g., 400 or 500 series errors)
                if (!response.IsSuccessStatusCode)
                {
                    // Read the response body as a string (error message from the API)
                    string errorResponseBody = await response.Content.ReadAsStringAsync();

                    // Directly show the response body content (without showing HTTP status)
                    lblResult.Text = errorResponseBody;

                    return;  // Exit early as the error has been handled
                }

                // If the status code is 200 OK, process the response
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response from the API
                JObject parsedJson = JObject.Parse(responseBody);
                string message = parsedJson["message"]?.ToString()?.Trim();  // Extract the "message" field

                // Check if the message from the API is not null or empty
                if (!string.IsNullOrEmpty(message))
                {
                    Console.WriteLine($"Parsed message: {message}");

                    // Handle the license validation response based on the "message"
                    if (message.Equals("License is valid.", StringComparison.OrdinalIgnoreCase))
                    {
                        // Deserialize SubCompanies from the response
                        var subCompanies = parsedJson["subCompanies"]?.ToObject<List<SubCompany>>() ?? new List<SubCompany>();

                        // Call method to populate the ComboBox with sub-companies
                        PopulateSubCompanies(subCompanies);

                        // Show success message
                        lblResult.Text = "License validated successfully.";
                    }
                    else if (message.Equals("License expired.", StringComparison.OrdinalIgnoreCase))
                    {
                        // Show specific message for expired license
                        lblResult.Text = "The license has expired. Please renew your license.";
                    }
                    else
                    {
                        // Show general invalid license message for any other response
                        lblResult.Text = "The license key is invalid. Please check the key or contact support.";
                    }
                }
                else
                {
                    // If the API response doesn't contain a message field
                    lblResult.Text = "The server response is missing a message. Please contact support.";
                }
            }
            catch (HttpRequestException ex)
            {
                // Catch HTTP-related issues like network errors, unreachable API, etc.
                lblResult.Text = "Network error occurred. Please check your connection or try again later.";
                Console.WriteLine($"HttpRequestException: {ex.Message}");  // Log detailed error for developers
            }
            catch (JsonException ex)
            {
                // Handle JSON parsing errors (invalid JSON format from API)
                lblResult.Text = "Error parsing server response. Please contact support.";
                Console.WriteLine($"JsonException: {ex.Message}");  // Log detailed error for developers
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors
                lblResult.Text = "An unexpected error occurred. Please try again later.";
                Console.WriteLine($"Exception: {ex.Message}");  // Log detailed error for developers
            }
        }





        // Method to populate the ComboBox with SubCompanies
        private void PopulateSubCompanies(List<SubCompany> subCompanies)
        {
            comboBoxSubCompanies.Items.Clear();

            // Add the "Select a sub-company" option at the top
            comboBoxSubCompanies.Items.Add("Select a sub-company");

            if (subCompanies.Count == 0)
            {
                comboBoxSubCompanies.Items.Add("No sub-companies available.");
                lblResult.Text = "No sub-companies found.";
                return;
            }

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
                    Form2 subCompanyDetailsForm = new Form2(validationResponse, selectedId.ToString()); // Ensure the second param is a string
                    subCompanyDetailsForm.Show();  // Show Form2 with the validation result and sub-company details
                }
            }
            else
            {
                // Handle the case where "Select a sub-company" is still selected
                lblResult.Text = "Please select a valid sub-company.";
            }
        }
    }

    // Class representing a SubCompany
    public class SubCompany
    {
        public int subCompanyId { get; set; }
        public string subCompanyName { get; set; }
    }

    // Helper class to represent ComboBox item (text and value pair)
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text; // Display the SubCompanyName in the ComboBox
        }
    }
}
