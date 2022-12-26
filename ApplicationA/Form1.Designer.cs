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
            this.btnCreateApplication = new System.Windows.Forms.Button();
            this.textBoxApplicationName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateModule = new System.Windows.Forms.Button();
            this.textBoxModuleName = new System.Windows.Forms.TextBox();
            this.applicationsList = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateSub = new System.Windows.Forms.Button();
            this.textBoxEndpoint = new System.Windows.Forms.TextBox();
            this.modulesList = new System.Windows.Forms.ListBox();
            this.checkedListBoxSubType = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            // textBoxApplicationName
            // 
            this.textBoxApplicationName.Location = new System.Drawing.Point(7, 22);
            this.textBoxApplicationName.Name = "textBoxApplicationName";
            this.textBoxApplicationName.Size = new System.Drawing.Size(150, 22);
            this.textBoxApplicationName.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnCreateModule);
            this.groupBox2.Controls.Add(this.textBoxModuleName);
            this.groupBox2.Controls.Add(this.applicationsList);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 229);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Module";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Applications:";
            // 
            // btnCreateModule
            // 
            this.btnCreateModule.Location = new System.Drawing.Point(185, 183);
            this.btnCreateModule.Name = "btnCreateModule";
            this.btnCreateModule.Size = new System.Drawing.Size(75, 23);
            this.btnCreateModule.TabIndex = 2;
            this.btnCreateModule.Text = "Create";
            this.btnCreateModule.UseVisualStyleBackColor = true;
            this.btnCreateModule.Click += new System.EventHandler(this.btnCreateModule_Click);
            // 
            // textBoxModuleName
            // 
            this.textBoxModuleName.Location = new System.Drawing.Point(6, 184);
            this.textBoxModuleName.Name = "textBoxModuleName";
            this.textBoxModuleName.Size = new System.Drawing.Size(151, 22);
            this.textBoxModuleName.TabIndex = 1;
            // 
            // applicationsList
            // 
            this.applicationsList.FormattingEnabled = true;
            this.applicationsList.ItemHeight = 16;
            this.applicationsList.Location = new System.Drawing.Point(7, 54);
            this.applicationsList.Name = "applicationsList";
            this.applicationsList.Size = new System.Drawing.Size(218, 100);
            this.applicationsList.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.checkedListBoxSubType);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBoxEndpoint);
            this.groupBox3.Controls.Add(this.btnCreateSub);
            this.groupBox3.Controls.Add(this.modulesList);
            this.groupBox3.Location = new System.Drawing.Point(304, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(465, 179);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Subscription";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Module";
            // 
            // btnCreateSub
            // 
            this.btnCreateSub.Location = new System.Drawing.Point(384, 132);
            this.btnCreateSub.Name = "btnCreateSub";
            this.btnCreateSub.Size = new System.Drawing.Size(75, 23);
            this.btnCreateSub.TabIndex = 2;
            this.btnCreateSub.Text = "Create";
            this.btnCreateSub.UseVisualStyleBackColor = true;
            // 
            // textBoxEndpoint
            // 
            this.textBoxEndpoint.Location = new System.Drawing.Point(209, 132);
            this.textBoxEndpoint.Name = "textBoxEndpoint";
            this.textBoxEndpoint.Size = new System.Drawing.Size(151, 22);
            this.textBoxEndpoint.TabIndex = 1;
            // 
            // modulesList
            // 
            this.modulesList.FormattingEnabled = true;
            this.modulesList.ItemHeight = 16;
            this.modulesList.Location = new System.Drawing.Point(7, 54);
            this.modulesList.Name = "modulesList";
            this.modulesList.Size = new System.Drawing.Size(169, 100);
            this.modulesList.TabIndex = 0;
            // 
            // checkedListBoxSubType
            // 
            this.checkedListBoxSubType.FormattingEnabled = true;
            this.checkedListBoxSubType.Location = new System.Drawing.Point(209, 54);
            this.checkedListBoxSubType.Name = "checkedListBoxSubType";
            this.checkedListBoxSubType.Size = new System.Drawing.Size(134, 38);
            this.checkedListBoxSubType.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sub Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Endpoint";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 339);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreateApplication;
        private System.Windows.Forms.TextBox textBoxApplicationName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateModule;
        private System.Windows.Forms.TextBox textBoxModuleName;
        private System.Windows.Forms.ListBox applicationsList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateSub;
        private System.Windows.Forms.TextBox textBoxEndpoint;
        private System.Windows.Forms.ListBox modulesList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox checkedListBoxSubType;
    }
}

