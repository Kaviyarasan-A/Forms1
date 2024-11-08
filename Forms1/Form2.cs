using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Forms1
{
    public partial class Form2 : Form
    {
        private string validationResult;

        // Constructor that accepts the validation result (response)
        public Form2(string responseData)
        {
            InitializeComponent();
            validationResult = responseData;

            // Display the result in a label or a textbox in Form2
            lblValidationResult.Text = validationResult;
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
                File.WriteAllText(filePath, xmlContent);

                MessageBox.Show("File saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Method to convert the response into XML format
        private string ConvertToXml(string responseData)
        {
            // Create a new XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            // Optionally, wrap the response in an XML root element
            XmlElement rootElement = xmlDoc.CreateElement("LicenseValidationResult");
            xmlDoc.AppendChild(rootElement);

            // Add the response as a child element
            XmlElement responseElement = xmlDoc.CreateElement("Result");
            responseElement.InnerText = responseData;
            rootElement.AppendChild(responseElement);

            // Convert XmlDocument to string
            using (StringWriter stringWriter = new StringWriter())
            using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
            {
                xmlWriter.Formatting = Formatting.Indented;
                xmlDoc.WriteTo(xmlWriter);
                return stringWriter.ToString();
            }
        }
    }
}
