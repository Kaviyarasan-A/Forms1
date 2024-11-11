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

                // Clear the ComboBox before performing the validation to ensure it's empty for new data
                comboBoxSubCompanies.Items.Clear();
                comboBoxSubCompanies.Items.Add("Select a sub-company"); // Optional: Add default message when no sub-companies

                // Properly build the URL with URL encoding for the query parameter
                string url = $"https://localhost:44395/api/LicenseValidation/ValidateLicense/validate?licenceKey={Uri.EscapeDataString(licenseKey)}";

                // Make the HTTP request and get the full response
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

                string message = parsedJson["message"]?.ToString()?.Trim(); // Extract the "message" field

                if (!string.IsNullOrEmpty(message))
                {
                    if (message.Equals("License is valid, but no sub-companies available.", StringComparison.OrdinalIgnoreCase))
                    {
                        string companyName = parsedJson["companyName"]?.ToString();
                        string apiLicenseKey = parsedJson["licenseKey"]?.ToString();  // Renamed this to apiLicenseKey to avoid conflict
                        string validFrom = parsedJson["validFrom"]?.ToString();
                        string validTo = parsedJson["validTo"]?.ToString();

                        // When no sub-companies are available, redirect to Form2 with the relevant data
                        Form2 noSubCompanyForm = new Form2(message, companyName, apiLicenseKey, validFrom, validTo);
                        noSubCompanyForm.Show();
                        return; // Exit early as Form2 has been opened
                    }

                    // Handle other cases like "License expired" or "Invalid license"
                    if (message.Equals("License expired.", StringComparison.OrdinalIgnoreCase))
                    {
                        lblResult.Text = "The license has expired. Please renew your license.";
                        comboBoxSubCompanies.Items.Clear();
                    }
                    else if (message.Equals("License is valid.", StringComparison.OrdinalIgnoreCase))
                    {
                        var subCompanies = parsedJson["subCompanies"]?.ToObject<List<SubCompany>>() ?? new List<SubCompany>();

                        if (subCompanies.Count == 0)
                        {
                            // When no sub-companies are available, redirect to Form2 with the message and license key
                            Form2 noSubCompanyForm = new Form2(message, "No sub-companies available.", licenseKey, "", "");
                            noSubCompanyForm.Show();
                            return; // Exit early as Form2 has been opened
                        }

                        PopulateSubCompanies(subCompanies);  // Populate the ComboBox with sub-companies
                        lblResult.Text = "License validated successfully.";
                    }
                    else
                    {
                        lblResult.Text = "The license key is invalid. Please check the key or contact support.";
                        comboBoxSubCompanies.Items.Clear();
                    }
                }
                else
                {
                    lblResult.Text = "The server response is missing a message. Please contact support.";
                    comboBoxSubCompanies.Items.Clear();
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
