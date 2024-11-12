using System;
using System.Windows.Forms;

namespace Forms1
{
    public partial class Form2 : Form
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblValidationResult;
        private Label lblSubCompanyDetails;
        private Label lblSubCompanyName;
        private Label lblConnectionStringOnline;
        private Label lblConnectionStringOffline;
        private Label lblSubCompanyId;
        private Label lblLicenseKey;
        private Label lblDownloadStatus;
        private Label lblCompanyName;

        // Add a variable to track download status
        private bool isFileDownloaded = false;

        public Form2()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblValidationResult = new System.Windows.Forms.Label();
            this.lblSubCompanyDetails = new System.Windows.Forms.Label();
            this.lblSubCompanyName = new System.Windows.Forms.Label();
            this.lblConnectionStringOnline = new System.Windows.Forms.Label();
            this.lblConnectionStringOffline = new System.Windows.Forms.Label();
            this.lblSubCompanyId = new System.Windows.Forms.Label();
            this.lblLicenseKey = new System.Windows.Forms.Label();
            this.lblDownloadStatus = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblValidationResult
            // 
            this.lblValidationResult.AutoSize = true;
            this.lblValidationResult.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblValidationResult.Location = new System.Drawing.Point(30, 30);
            this.lblValidationResult.Name = "lblValidationResult";
            this.lblValidationResult.Size = new System.Drawing.Size(263, 29);
            this.lblValidationResult.TabIndex = 0;
            this.lblValidationResult.Text = "Validation result:";
            // 
            // lblSubCompanyDetails
            // 
            this.lblSubCompanyDetails.AutoSize = true;
            this.lblSubCompanyDetails.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.lblSubCompanyDetails.Location = new System.Drawing.Point(30, 90);
            this.lblSubCompanyDetails.Name = "lblSubCompanyDetails";
            this.lblSubCompanyDetails.Size = new System.Drawing.Size(425, 23);
            this.lblSubCompanyDetails.TabIndex = 1;
            this.lblSubCompanyDetails.Text = "Fetching sub-company details...";
            // 
            // lblSubCompanyName
            // 
            this.lblSubCompanyName.AutoSize = true;
            this.lblSubCompanyName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.lblSubCompanyName.Location = new System.Drawing.Point(30, 150);
            this.lblSubCompanyName.Name = "lblSubCompanyName";
            this.lblSubCompanyName.Size = new System.Drawing.Size(213, 23);
            this.lblSubCompanyName.TabIndex = 2;
            this.lblSubCompanyName.Text = "SubCompany: ";
            // 
            // lblConnectionStringOnline
            // 
            this.lblConnectionStringOnline.AutoSize = true;
            this.lblConnectionStringOnline.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.lblConnectionStringOnline.Location = new System.Drawing.Point(30, 210);
            this.lblConnectionStringOnline.Name = "lblConnectionStringOnline";
            this.lblConnectionStringOnline.Size = new System.Drawing.Size(308, 23);
            this.lblConnectionStringOnline.TabIndex = 3;
            this.lblConnectionStringOnline.Text = "Online Connection: ";
            // 
            // lblConnectionStringOffline
            // 
            this.lblConnectionStringOffline.AutoSize = true;
            this.lblConnectionStringOffline.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.lblConnectionStringOffline.Location = new System.Drawing.Point(30, 270);
            this.lblConnectionStringOffline.Name = "lblConnectionStringOffline";
            this.lblConnectionStringOffline.Size = new System.Drawing.Size(333, 23);
            this.lblConnectionStringOffline.TabIndex = 4;
            this.lblConnectionStringOffline.Text = "Offline Connection: ";
            // 
            // lblSubCompanyId
            // 
            this.lblSubCompanyId.AutoSize = true;
            this.lblSubCompanyId.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.lblSubCompanyId.Location = new System.Drawing.Point(30, 330);
            this.lblSubCompanyId.Name = "lblSubCompanyId";
            this.lblSubCompanyId.Size = new System.Drawing.Size(227, 23);
            this.lblSubCompanyId.TabIndex = 5;
            this.lblSubCompanyId.Text = "SubCompany ID: ";
            // 
            // lblLicenseKey
            // 
            this.lblLicenseKey.AutoSize = true;
            this.lblLicenseKey.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.lblLicenseKey.Location = new System.Drawing.Point(30, 390);
            this.lblLicenseKey.Name = "lblLicenseKey";
            this.lblLicenseKey.Size = new System.Drawing.Size(177, 23);
            this.lblLicenseKey.TabIndex = 6;
            this.lblLicenseKey.Text = "License Key:";
            // 
            // lblDownloadStatus
            // 
            this.lblDownloadStatus.AutoSize = true;
            this.lblDownloadStatus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.lblDownloadStatus.Location = new System.Drawing.Point(30, 450);
            this.lblDownloadStatus.Name = "lblDownloadStatus";
            this.lblDownloadStatus.Size = new System.Drawing.Size(245, 23);
            this.lblDownloadStatus.TabIndex = 7;
            this.lblDownloadStatus.Text = "Download Status:";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.lblCompanyName.Location = new System.Drawing.Point(30, 510);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(227, 23);
            this.lblCompanyName.TabIndex = 8;
            this.lblCompanyName.Text = "Company Name:";
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.lblDownloadStatus);
            this.Controls.Add(this.lblLicenseKey);
            this.Controls.Add(this.lblSubCompanyId);
            this.Controls.Add(this.lblConnectionStringOffline);
            this.Controls.Add(this.lblConnectionStringOnline);
            this.Controls.Add(this.lblSubCompanyName);
            this.Controls.Add(this.lblSubCompanyDetails);
            this.Controls.Add(this.lblValidationResult);
            this.Name = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Method to simulate the download process
        private void DownloadFile()
        {
            // Simulate download (this is where your file download logic would go)
            System.Threading.Thread.Sleep(2000); // Simulating a 2-second download delay

            // After download completes, trigger the message box
            ShowDownloadMessage();
        }

        // Method to show a message box after file is downloaded
        private void ShowDownloadMessage()
        {
            MessageBox.Show("The file has been downloaded successfully!", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // You can call DownloadFile() when you detect the download is done in your code
        // For example, if you download the file automatically when the form is loaded:
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DownloadFile(); // Simulate file download when the form loads
        }
    }
}
