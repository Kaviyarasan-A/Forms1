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
        private System.Windows.Forms.Button btnDownloadXML;

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
            this.btnDownloadXML = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // lblValidationResult
            // 
            this.lblValidationResult.AutoSize = true;
            this.lblValidationResult.Font = new System.Drawing.Font("Segoe UI", 18F);  // Increased font size
            this.lblValidationResult.Location = new System.Drawing.Point(20, 20);  // Positioned a bit more spaced
            this.lblValidationResult.Name = "lblValidationResult";
            this.lblValidationResult.Size = new System.Drawing.Size(240, 32);  // Adjusted label size
            this.lblValidationResult.TabIndex = 0;
            this.lblValidationResult.Text = "Validation result: N/A";

            // 
            // lblSubCompanyDetails
            // 
            this.lblSubCompanyDetails.AutoSize = true;
            this.lblSubCompanyDetails.Font = new System.Drawing.Font("Segoe UI", 18F);  // Larger font size
            this.lblSubCompanyDetails.Location = new System.Drawing.Point(20, 60);
            this.lblSubCompanyDetails.Name = "lblSubCompanyDetails";
            this.lblSubCompanyDetails.Size = new System.Drawing.Size(350, 32);  // Adjusted label size
            this.lblSubCompanyDetails.TabIndex = 1;
            this.lblSubCompanyDetails.Text = "Fetching sub-company details...";

            // 
            // lblSubCompanyName
            // 
            this.lblSubCompanyName.AutoSize = true;
            this.lblSubCompanyName.Font = new System.Drawing.Font("Segoe UI", 18F);  // Increased font size
            this.lblSubCompanyName.Location = new System.Drawing.Point(20, 100);  // Adjusted position
            this.lblSubCompanyName.Name = "lblSubCompanyName";
            this.lblSubCompanyName.Size = new System.Drawing.Size(265, 32);  // Adjusted label size
            this.lblSubCompanyName.TabIndex = 2;
            this.lblSubCompanyName.Text = "SubCompany: N/A";

            // 
            // lblConnectionStringOnline
            // 
            this.lblConnectionStringOnline.AutoSize = true;
            this.lblConnectionStringOnline.Font = new System.Drawing.Font("Segoe UI", 18F);  // Larger font size
            this.lblConnectionStringOnline.Location = new System.Drawing.Point(20, 140);  // Adjusted position
            this.lblConnectionStringOnline.Name = "lblConnectionStringOnline";
            this.lblConnectionStringOnline.Size = new System.Drawing.Size(320, 32);  // Adjusted label size
            this.lblConnectionStringOnline.TabIndex = 3;
            this.lblConnectionStringOnline.Text = "Online Connection: N/A";

            // 
            // lblConnectionStringOffline
            // 
            this.lblConnectionStringOffline.AutoSize = true;
            this.lblConnectionStringOffline.Font = new System.Drawing.Font("Segoe UI", 18F);  // Larger font size
            this.lblConnectionStringOffline.Location = new System.Drawing.Point(20, 180);  // Adjusted position
            this.lblConnectionStringOffline.Name = "lblConnectionStringOffline";
            this.lblConnectionStringOffline.Size = new System.Drawing.Size(330, 32);  // Adjusted label size
            this.lblConnectionStringOffline.TabIndex = 4;
            this.lblConnectionStringOffline.Text = "Offline Connection: N/A";

            // 
            // lblSubCompanyId
            // 
            this.lblSubCompanyId.AutoSize = true;
            this.lblSubCompanyId.Font = new System.Drawing.Font("Segoe UI", 18F);  // Larger font size
            this.lblSubCompanyId.Location = new System.Drawing.Point(20, 220);  // Adjusted position
            this.lblSubCompanyId.Name = "lblSubCompanyId";
            this.lblSubCompanyId.Size = new System.Drawing.Size(230, 32);  // Adjusted label size
            this.lblSubCompanyId.TabIndex = 5;
            this.lblSubCompanyId.Text = "SubCompany ID: N/A";

            // 
            // btnDownloadXML
            // 
            this.btnDownloadXML.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);  // Increased font size
            this.btnDownloadXML.Location = new System.Drawing.Point(20, 260);  // Positioned at the bottom
            this.btnDownloadXML.Name = "btnDownloadXML";
            this.btnDownloadXML.Size = new System.Drawing.Size(300, 50);  // Adjusted size
            this.btnDownloadXML.TabIndex = 6;
            this.btnDownloadXML.Text = "Download as XML";
            this.btnDownloadXML.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDownloadXML.ForeColor = System.Drawing.Color.White;
            this.btnDownloadXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadXML.UseVisualStyleBackColor = true;
            this.btnDownloadXML.Click += new System.EventHandler(this.btnDownloadXML_Click);

            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);  // Full-screen window
            this.Controls.Add(this.btnDownloadXML);
            this.Controls.Add(this.lblSubCompanyId);
            this.Controls.Add(this.lblConnectionStringOffline);
            this.Controls.Add(this.lblConnectionStringOnline);
            this.Controls.Add(this.lblSubCompanyName);
            this.Controls.Add(this.lblSubCompanyDetails);
            this.Controls.Add(this.lblValidationResult);
            this.Name = "Form2";
            this.Text = "SubCompany Details";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;  // Maximized form
            this.BackColor = System.Drawing.Color.WhiteSmoke;  // Set a clean background color
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
