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
        private System.Windows.Forms.Label lblDownloadStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblLicenseKey; // New label for license key

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
            this.lblDownloadStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblLicenseKey = new System.Windows.Forms.Label(); // Initialize new label

            this.SuspendLayout();

            // 
            // lblValidationResult
            // 
            this.lblValidationResult.AutoSize = true;
            this.lblValidationResult.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblValidationResult.Location = new System.Drawing.Point(20, 30);
            this.lblValidationResult.Name = "lblValidationResult";
            this.lblValidationResult.Size = new System.Drawing.Size(200, 25);
            this.lblValidationResult.TabIndex = 0;
            this.lblValidationResult.Text = "Validation Result: N/A";

            // 
            // lblSubCompanyDetails
            // 
            this.lblSubCompanyDetails.AutoSize = true;
            this.lblSubCompanyDetails.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSubCompanyDetails.Location = new System.Drawing.Point(20, 70);
            this.lblSubCompanyDetails.Name = "lblSubCompanyDetails";
            this.lblSubCompanyDetails.Size = new System.Drawing.Size(350, 25);
            this.lblSubCompanyDetails.TabIndex = 1;
            this.lblSubCompanyDetails.Text = "Fetching sub-company details...";

            // 
            // lblSubCompanyName
            // 
            this.lblSubCompanyName.AutoSize = true;
            this.lblSubCompanyName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSubCompanyName.Location = new System.Drawing.Point(20, 110);
            this.lblSubCompanyName.Name = "lblSubCompanyName";
            this.lblSubCompanyName.Size = new System.Drawing.Size(240, 25);
            this.lblSubCompanyName.TabIndex = 2;
            this.lblSubCompanyName.Text = "SubCompany: N/A";

            // 
            // lblConnectionStringOnline
            // 
            this.lblConnectionStringOnline.AutoSize = true;
            this.lblConnectionStringOnline.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStringOnline.Location = new System.Drawing.Point(20, 150);
            this.lblConnectionStringOnline.Name = "lblConnectionStringOnline";
            this.lblConnectionStringOnline.Size = new System.Drawing.Size(320, 25);
            this.lblConnectionStringOnline.TabIndex = 3;
            this.lblConnectionStringOnline.Text = "Online Connection: N/A";

            // 
            // lblConnectionStringOffline
            // 
            this.lblConnectionStringOffline.AutoSize = true;
            this.lblConnectionStringOffline.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStringOffline.Location = new System.Drawing.Point(20, 190);
            this.lblConnectionStringOffline.Name = "lblConnectionStringOffline";
            this.lblConnectionStringOffline.Size = new System.Drawing.Size(330, 25);
            this.lblConnectionStringOffline.TabIndex = 4;
            this.lblConnectionStringOffline.Text = "Offline Connection: N/A";

            // 
            // lblSubCompanyId
            // 
            this.lblSubCompanyId.AutoSize = true;
            this.lblSubCompanyId.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSubCompanyId.Location = new System.Drawing.Point(20, 230);
            this.lblSubCompanyId.Name = "lblSubCompanyId";
            this.lblSubCompanyId.Size = new System.Drawing.Size(230, 25);
            this.lblSubCompanyId.TabIndex = 5;
            this.lblSubCompanyId.Text = "SubCompany ID: N/A";

            // 
            // lblDownloadStatus
            // 
            this.lblDownloadStatus.AutoSize = true;
            this.lblDownloadStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblDownloadStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblDownloadStatus.Location = new System.Drawing.Point(20, 270);
            this.lblDownloadStatus.Name = "lblDownloadStatus";
            this.lblDownloadStatus.Size = new System.Drawing.Size(220, 25);
            this.lblDownloadStatus.TabIndex = 6;
            this.lblDownloadStatus.Text = "Download Status: N/A";

            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 310);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(320, 30);
            this.progressBar.TabIndex = 7;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;  // Show moving animation when downloading

            // 
            // lblLicenseKey
            // 
            this.lblLicenseKey.AutoSize = true;
            this.lblLicenseKey.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLicenseKey.Location = new System.Drawing.Point(20, 350);  // Position below the download status
            this.lblLicenseKey.Name = "lblLicenseKey";
            this.lblLicenseKey.Size = new System.Drawing.Size(200, 25);
            this.lblLicenseKey.TabIndex = 8;
            this.lblLicenseKey.Text = "License Key: N/A";  // Default text

            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(380, 460);
            this.Controls.Add(this.lblLicenseKey);  // Add new label to the form
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblDownloadStatus);
            this.Controls.Add(this.lblSubCompanyId);
            this.Controls.Add(this.lblConnectionStringOffline);
            this.Controls.Add(this.lblConnectionStringOnline);
            this.Controls.Add(this.lblSubCompanyName);
            this.Controls.Add(this.lblSubCompanyDetails);
            this.Controls.Add(this.lblValidationResult);
            this.Name = "Form2";
            this.Text = "SubCompany Details";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
