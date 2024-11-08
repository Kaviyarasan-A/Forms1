namespace Forms1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ComboBox comboBoxSubCompanies;  // Added ComboBox for SubCompanies
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
            this.comboBoxSubCompanies = new System.Windows.Forms.ComboBox();  // ComboBox initialization
            this.label2 = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // textBox2 (License Key)
            this.textBox2.Location = new System.Drawing.Point(16, 37);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 22);
            this.textBox2.TabIndex = 0;

            // button1 (Validate License)
            this.button1.Location = new System.Drawing.Point(16, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Validate License";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // label1 (Enter License Key)
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter License Key:";

            // lblResult (Validation Result)
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(16, 120);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(51, 17);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "Result: ";

            // comboBoxSubCompanies (Sub-Companies)
            this.comboBoxSubCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubCompanies.FormattingEnabled = true;
            this.comboBoxSubCompanies.Location = new System.Drawing.Point(16, 160); // Adjusted position
            this.comboBoxSubCompanies.Name = "comboBoxSubCompanies";
            this.comboBoxSubCompanies.Size = new System.Drawing.Size(200, 24); // Adjust size
            this.comboBoxSubCompanies.TabIndex = 4;

            // label2 (Empty Label, optional)
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 5;

            // Form1 (Main form)
            this.ClientSize = new System.Drawing.Size(240, 240);  // Adjusted size to fit all components
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxSubCompanies);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Name = "Form1";
            this.Text = "License Validation";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
