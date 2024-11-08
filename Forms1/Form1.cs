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

                    if (message.Equals("License is valid.", StringComparison.OrdinalIgnoreCase))
                    {
                        // Deserialize SubCompanies from the response
                        var subCompanies = parsedJson["subCompanies"]?.ToObject<List<SubCompany>>() ?? new List<SubCompany>();

                        // Call method to populate the ComboBox with sub-companies
                        PopulateSubCompanies(subCompanies);

                        lblResult.Text = "License validated successfully.";
                    }
                    else
                    {
                        lblResult.Text = $"Validation failed: {message}";
                    }
                }
                else
                {
                    lblResult.Text = "Message field is missing or null in the response.";
                }
            }
            catch (HttpRequestException httpEx)
            {
                lblResult.Text = $"Request failed: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"An error occurred: {ex.Message}";
            }
        }

        // Method to populate the ComboBox with SubCompanies
        private void PopulateSubCompanies(List<SubCompany> subCompanies)
        {
            comboBoxSubCompanies.Items.Clear();

            if (subCompanies.Count == 0)
            {
                // If no sub-companies, inform the user
                comboBoxSubCompanies.Items.Add("No sub-companies available.");
                lblResult.Text = "No sub-companies found.";
                return;
            }

            // Add each SubCompany to the ComboBox
            foreach (var SubCompany in subCompanies)
            {
                comboBoxSubCompanies.Items.Add(new ComboBoxItem
                {
                    Text = SubCompany.subCompanyName,  // Display the name
                    Value = SubCompany.subCompanyId    // Store the ID in the Value
                });
            }

            // Set the default selected item (first item, if available)
            comboBoxSubCompanies.SelectedIndex = 0;
        }

        // ComboBox selection changed event handler
        private void comboBoxSubCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSubCompanies.SelectedItem is ComboBoxItem selectedItem)
            {
                int selectedId = selectedItem.Value;
                MessageBox.Show($"Selected SubCompany ID: {selectedId}");
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
