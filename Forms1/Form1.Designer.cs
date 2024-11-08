﻿namespace Forms1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ComboBox comboBoxSubCompanies;
        private System.Windows.Forms.Label label2;

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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.comboBoxSubCompanies = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // textBox2 (License Key Input)
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 10F);  // Modern font and size
            this.textBox2.Location = new System.Drawing.Point(16, 37);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 26);  // Adjust size for better appearance
            this.textBox2.TabIndex = 0;
              // Placeholder text

            // button1 (Validate License Button)
            this.button1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(16, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 40);  // Larger button for better clickability
            this.button1.TabIndex = 1;
            this.button1.Text = "Validate License";
            this.button1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;  // Flat style for modern look
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // label1 (Enter License Key Label)
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter License Key:";

            // lblResult (Result Label)
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblResult.Location = new System.Drawing.Point(16, 120);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(56, 19);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "Result: ";

            // comboBoxSubCompanies (Dropdown for Sub-Companies)
            this.comboBoxSubCompanies.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxSubCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubCompanies.FormattingEnabled = true;
            this.comboBoxSubCompanies.Location = new System.Drawing.Point(16, 160);
            this.comboBoxSubCompanies.Name = "comboBoxSubCompanies";
            this.comboBoxSubCompanies.Size = new System.Drawing.Size(200, 26);
            this.comboBoxSubCompanies.TabIndex = 4;
            this.comboBoxSubCompanies.SelectedIndexChanged += new System.EventHandler(this.comboBoxSubCompanies_SelectedIndexChanged);

            // label2 (Optional label, can be used for any additional text or info)
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(16, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 19);
            this.label2.TabIndex = 5;

            // Form1
            this.ClientSize = new System.Drawing.Size(240, 250);  // Adjusted size to fit all components nicely
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxSubCompanies);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Name = "Form1";
            this.Text = "License Validation";
            this.BackColor = System.Drawing.Color.WhiteSmoke;  // Set background color for a clean look
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
