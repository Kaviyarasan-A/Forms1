namespace Forms1
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;

        // Declare all the controls used in the form
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblLicenseKey;
        private System.Windows.Forms.Label lblSubCompanyName;
        private System.Windows.Forms.Label lblConnectionStringOnline;
        private System.Windows.Forms.Label lblConnectionStringOffline;
        private System.Windows.Forms.Label lblSubCompanyId;
        private System.Windows.Forms.Label lblSubCompanyDetails;
        private System.Windows.Forms.Label lblDownloadStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblValidationResult;

        // Constructor to initialize the form
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
            this.lblCompanyName = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // 
            // lblValidationResult
            // 
            this.lblValidationResult.AutoSize = true;
            this.lblValidationResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblValidationResult.Location = new System.Drawing.Point(20, 30);
            this.lblValidationResult.Name = "lblValidationResult";
            this.lblValidationResult.Size = new System.Drawing.Size(160, 21);
            this.lblValidationResult.TabIndex = 0;
            this.lblValidationResult.Text = "Validation result: ";

            // 
            // lblSubCompanyDetails
            // 
            this.lblSubCompanyDetails.AutoSize = true;
            this.lblSubCompanyDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSubCompanyDetails.Location = new System.Drawing.Point(20, 70);
            this.lblSubCompanyDetails.Name = "lblSubCompanyDetails";
            this.lblSubCompanyDetails.Size = new System.Drawing.Size(175, 21);
            this.lblSubCompanyDetails.TabIndex = 1;
            this.lblSubCompanyDetails.Text = "Sub-company Details:";

            // 
            // lblSubCompanyName
            // 
            this.lblSubCompanyName.AutoSize = true;
            this.lblSubCompanyName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubCompanyName.Location = new System.Drawing.Point(40, 110);
            this.lblSubCompanyName.Name = "lblSubCompanyName";
            this.lblSubCompanyName.Size = new System.Drawing.Size(150, 19);
            this.lblSubCompanyName.TabIndex = 2;
            this.lblSubCompanyName.Text = "Sub-company Name: ";

            // 
            // lblConnectionStringOnline
            // 
            this.lblConnectionStringOnline.AutoSize = true;
            this.lblConnectionStringOnline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConnectionStringOnline.Location = new System.Drawing.Point(40, 140);
            this.lblConnectionStringOnline.Name = "lblConnectionStringOnline";
            this.lblConnectionStringOnline.Size = new System.Drawing.Size(170, 19);
            this.lblConnectionStringOnline.TabIndex = 3;
            this.lblConnectionStringOnline.Text = "Online Connection: ";

            // 
            // lblConnectionStringOffline
            // 
            this.lblConnectionStringOffline.AutoSize = true;
            this.lblConnectionStringOffline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConnectionStringOffline.Location = new System.Drawing.Point(40, 170);
            this.lblConnectionStringOffline.Name = "lblConnectionStringOffline";
            this.lblConnectionStringOffline.Size = new System.Drawing.Size(170, 19);
            this.lblConnectionStringOffline.TabIndex = 4;
            this.lblConnectionStringOffline.Text = "Offline Connection: ";

            // 
            // lblSubCompanyId
            // 
            this.lblSubCompanyId.AutoSize = true;
            this.lblSubCompanyId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubCompanyId.Location = new System.Drawing.Point(40, 200);
            this.lblSubCompanyId.Name = "lblSubCompanyId";
            this.lblSubCompanyId.Size = new System.Drawing.Size(130, 19);
            this.lblSubCompanyId.TabIndex = 5;
            this.lblSubCompanyId.Text = "Sub-company ID: ";

            // 
            // lblLicenseKey
            // 
            this.lblLicenseKey.AutoSize = true;
            this.lblLicenseKey.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLicenseKey.Location = new System.Drawing.Point(40, 230);
            this.lblLicenseKey.Name = "lblLicenseKey";
            this.lblLicenseKey.Size = new System.Drawing.Size(120, 19);
            this.lblLicenseKey.TabIndex = 6;
            this.lblLicenseKey.Text = "License Key: ";

            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 260);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(260, 25);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;

            // 
            // lblDownloadStatus
            // 
            this.lblDownloadStatus.AutoSize = true;
            this.lblDownloadStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblDownloadStatus.Location = new System.Drawing.Point(20, 300);
            this.lblDownloadStatus.Name = "lblDownloadStatus";
            this.lblDownloadStatus.Size = new System.Drawing.Size(130, 19);
            this.lblDownloadStatus.TabIndex = 8;
            this.lblDownloadStatus.Text = "Download Status: ";

            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCompanyName.Location = new System.Drawing.Point(40, 350);  // Adjust position as necessary
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(150, 19);
            this.lblCompanyName.TabIndex = 9;
            this.lblCompanyName.Text = "Company Name: ";

            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(800, 500); // Adjust form size as needed
            this.Controls.Add(this.lblValidationResult);
            this.Controls.Add(this.lblSubCompanyDetails);
            this.Controls.Add(this.lblSubCompanyName);
            this.Controls.Add(this.lblConnectionStringOnline);
            this.Controls.Add(this.lblConnectionStringOffline);
            this.Controls.Add(this.lblSubCompanyId);
            this.Controls.Add(this.lblLicenseKey);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblDownloadStatus);
            this.Controls.Add(this.lblCompanyName);
            this.Name = "Form2";
            this.Text = "License Validation and Download";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
