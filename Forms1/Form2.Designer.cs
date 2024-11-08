namespace Forms1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblValidationResult = new System.Windows.Forms.Label();
            this.btnDownloadXML = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblValidationResult
            // 
            this.lblValidationResult.AutoSize = true;
            this.lblValidationResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidationResult.Location = new System.Drawing.Point(100, 50);
            this.lblValidationResult.Name = "lblValidationResult";
            this.lblValidationResult.Size = new System.Drawing.Size(0, 20);
            this.lblValidationResult.TabIndex = 0;
            // 
            // btnDownloadXML
            // 
            this.btnDownloadXML.Location = new System.Drawing.Point(300, 200);
            this.btnDownloadXML.Name = "btnDownloadXML";
            this.btnDownloadXML.Size = new System.Drawing.Size(150, 40);
            this.btnDownloadXML.TabIndex = 1;
            this.btnDownloadXML.Text = "Download as XML";
            this.btnDownloadXML.UseVisualStyleBackColor = true;
            this.btnDownloadXML.Click += new System.EventHandler(this.btnDownloadXML_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDownloadXML);
            this.Controls.Add(this.lblValidationResult);
            this.Name = "Form2";
            this.Text = "License Validation Result";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblValidationResult;
        private System.Windows.Forms.Button btnDownloadXML;
    }
}
