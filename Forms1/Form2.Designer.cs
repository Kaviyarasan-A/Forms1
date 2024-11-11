namespace Forms1
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblValidationResult;
        private System.Windows.Forms.Label lblSubCompanyDetails;
        private System.Windows.Forms.Label lblSubCompanyName;
        private System.Windows.Forms.Label lblConnectionStringOnline;
        private System.Windows.Forms.Label lblConnectionStringOffline;
        private System.Windows.Forms.Label lblSubCompanyId;
        private System.Windows.Forms.Label lblLicenseKey;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblDownloadStatus;
        private System.Windows.Forms.Label lblFooter;  // Added Footer Label

        // Constructor to initialize the form
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblDownloadStatus = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();  // Footer Label

            this.SuspendLayout();

            // lblValidationResult
            this.lblValidationResult.AutoSize = true;
            this.lblValidationResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblValidationResult.Location = new System.Drawing.Point(20, 30);
            this.lblValidationResult.Size = new System.Drawing.Size(160, 21);
            this.lblValidationResult.Text = "Validation Result:";

            // lblSubCompanyDetails
            this.lblSubCompanyDetails.AutoSize = true;
            this.lblSubCompanyDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSubCompanyDetails.Location = new System.Drawing.Point(20, 70);
            this.lblSubCompanyDetails.Size = new System.Drawing.Size(175, 21);
            this.lblSubCompanyDetails.Text = "Sub-company Details:";

            // lblSubCompanyName
            this.lblSubCompanyName.AutoSize = true;
            this.lblSubCompanyName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubCompanyName.Location = new System.Drawing.Point(40, 110);
            this.lblSubCompanyName.Size = new System.Drawing.Size(150, 19);
            this.lblSubCompanyName.Text = "Sub-company Name: ";

            // lblConnectionStringOnline
            this.lblConnectionStringOnline.AutoSize = true;
            this.lblConnectionStringOnline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConnectionStringOnline.Location = new System.Drawing.Point(40, 140);
            this.lblConnectionStringOnline.Size = new System.Drawing.Size(170, 19);
            this.lblConnectionStringOnline.Text = "Online Connection: ";

            // lblConnectionStringOffline
            this.lblConnectionStringOffline.AutoSize = true;
            this.lblConnectionStringOffline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConnectionStringOffline.Location = new System.Drawing.Point(40, 170);
            this.lblConnectionStringOffline.Size = new System.Drawing.Size(170, 19);
            this.lblConnectionStringOffline.Text = "Offline Connection: ";

            // lblSubCompanyId
            this.lblSubCompanyId.AutoSize = true;
            this.lblSubCompanyId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubCompanyId.Location = new System.Drawing.Point(40, 200);
            this.lblSubCompanyId.Size = new System.Drawing.Size(130, 19);
            this.lblSubCompanyId.Text = "Sub-company ID: ";

            // lblLicenseKey
            this.lblLicenseKey.AutoSize = true;
            this.lblLicenseKey.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLicenseKey.Location = new System.Drawing.Point(40, 230);
            this.lblLicenseKey.Size = new System.Drawing.Size(120, 19);
            this.lblLicenseKey.Text = "License Key: ";

            // progressBar
            this.progressBar.Location = new System.Drawing.Point(20, 260);
            this.progressBar.Size = new System.Drawing.Size(260, 25);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.Visible = false;

            // lblDownloadStatus
            this.lblDownloadStatus.AutoSize = true;
            this.lblDownloadStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblDownloadStatus.Location = new System.Drawing.Point(20, 300);
            this.lblDownloadStatus.Size = new System.Drawing.Size(130, 19);
            this.lblDownloadStatus.Text = "Download Status: ";

            

            // Form2
            this.ClientSize = new System.Drawing.Size(320, 350);
            this.Controls.Add(this.lblValidationResult);
            this.Controls.Add(this.lblSubCompanyDetails);
            this.Controls.Add(this.lblSubCompanyName);
            this.Controls.Add(this.lblConnectionStringOnline);
            this.Controls.Add(this.lblConnectionStringOffline);
            this.Controls.Add(this.lblSubCompanyId);
            this.Controls.Add(this.lblLicenseKey);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblDownloadStatus);
            this.Controls.Add(this.lblFooter);  // Add footer to the form
            this.Name = "Form2";
            this.Text = "License Validation and Download";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
