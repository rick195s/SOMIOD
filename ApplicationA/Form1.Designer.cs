namespace ApplicationA
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxApplicationName = new System.Windows.Forms.TextBox();
            this.btnCreateApplication = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateApplication);
            this.groupBox1.Controls.Add(this.textBoxApplicationName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application";
            // 
            // textBoxApplicationName
            // 
            this.textBoxApplicationName.Location = new System.Drawing.Point(7, 22);
            this.textBoxApplicationName.Name = "textBoxApplicationName";
            this.textBoxApplicationName.Size = new System.Drawing.Size(150, 22);
            this.textBoxApplicationName.TabIndex = 0;
            // 
            // btnCreateApplication
            // 
            this.btnCreateApplication.Location = new System.Drawing.Point(185, 21);
            this.btnCreateApplication.Name = "btnCreateApplication";
            this.btnCreateApplication.Size = new System.Drawing.Size(75, 23);
            this.btnCreateApplication.TabIndex = 1;
            this.btnCreateApplication.Text = "Create";
            this.btnCreateApplication.UseVisualStyleBackColor = true;
            this.btnCreateApplication.Click += new System.EventHandler(this.btnCreateApplication_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreateApplication;
        private System.Windows.Forms.TextBox textBoxApplicationName;
    }
}

