using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                var response = await client.GetStringAsync(url);

                // Parse the JSON response
                JObject parsedJson = JObject.Parse(response);
                string message = parsedJson["message"]?.ToString()?.Trim();

                if (!string.IsNullOrEmpty(message))
                {
                    Console.WriteLine($"Parsed message: {message}");

                    // Check the response message and display the appropriate message
                    if (message.Equals("License is valid.", StringComparison.OrdinalIgnoreCase))
                    {
                        // Deserialize SubCompanies from the response
                        var subCompanies = parsedJson["subCompanies"]?.ToObject<List<SubCompany>>() ?? new List<SubCompany>();

                        // Call method to populate the ComboBox with sub-companies
                        PopulateSubCompanies(subCompanies);

                        lblResult.Text = "License validated successfully.";
                    }
                    else if (message.Equals("License expired.", StringComparison.OrdinalIgnoreCase))
                    {
                        lblResult.Text = "License expired.";  // Show expired license message
                    }
                    else
                    {
                        lblResult.Text = "License invalid. Please check your license key.";
                    }
                }
                else
                {
                    lblResult.Text = "Response message is missing or null.";
                }
            }
            catch (HttpRequestException)
            {
                // Catch HTTP-related issues like network errors, unreachable API, etc.
                lblResult.Text = "License invalid. Please check your license key.";
            }
            catch (Exception)
            {
                // Catch any other errors like JSON parsing or unexpected exceptions
                lblResult.Text = "License invalid. Please check your license key.";
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
