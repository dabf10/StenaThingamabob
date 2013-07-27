namespace StenaThingamabob___Working_Title
{
    partial class MainForm
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
            this.m_CalculateButton = new System.Windows.Forms.Button();
            this.m_TabControl = new System.Windows.Forms.TabControl();
            this.m_StartTab = new System.Windows.Forms.TabPage();
            this.m_BrowseButton = new System.Windows.Forms.Button();
            this.m_DirectoryTextBox = new System.Windows.Forms.TextBox();
            this.m_SalaryTab = new System.Windows.Forms.TabPage();
            this.m_OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_NameTextBox = new System.Windows.Forms.TextBox();
            this.m_SalaryTextBox = new System.Windows.Forms.TextBox();
            this.m_TabControl.SuspendLayout();
            this.m_StartTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_CalculateButton
            // 
            this.m_CalculateButton.Location = new System.Drawing.Point(14, 157);
            this.m_CalculateButton.Name = "m_CalculateButton";
            this.m_CalculateButton.Size = new System.Drawing.Size(75, 23);
            this.m_CalculateButton.TabIndex = 0;
            this.m_CalculateButton.Text = "Calculate";
            this.m_CalculateButton.UseVisualStyleBackColor = true;
            this.m_CalculateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_TabControl
            // 
            this.m_TabControl.Controls.Add(this.m_StartTab);
            this.m_TabControl.Controls.Add(this.m_SalaryTab);
            this.m_TabControl.Location = new System.Drawing.Point(-6, -4);
            this.m_TabControl.Name = "m_TabControl";
            this.m_TabControl.SelectedIndex = 0;
            this.m_TabControl.Size = new System.Drawing.Size(258, 232);
            this.m_TabControl.TabIndex = 1;
            // 
            // m_StartTab
            // 
            this.m_StartTab.Controls.Add(this.m_SalaryTextBox);
            this.m_StartTab.Controls.Add(this.m_NameTextBox);
            this.m_StartTab.Controls.Add(this.m_BrowseButton);
            this.m_StartTab.Controls.Add(this.m_DirectoryTextBox);
            this.m_StartTab.Controls.Add(this.m_CalculateButton);
            this.m_StartTab.Location = new System.Drawing.Point(4, 22);
            this.m_StartTab.Name = "m_StartTab";
            this.m_StartTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_StartTab.Size = new System.Drawing.Size(250, 206);
            this.m_StartTab.TabIndex = 0;
            this.m_StartTab.Text = "Start";
            this.m_StartTab.UseVisualStyleBackColor = true;
            this.m_StartTab.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // m_BrowseButton
            // 
            this.m_BrowseButton.Location = new System.Drawing.Point(15, 117);
            this.m_BrowseButton.Name = "m_BrowseButton";
            this.m_BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.m_BrowseButton.TabIndex = 2;
            this.m_BrowseButton.Text = "Browse";
            this.m_BrowseButton.UseVisualStyleBackColor = true;
            this.m_BrowseButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // m_DirectoryTextBox
            // 
            this.m_DirectoryTextBox.Location = new System.Drawing.Point(15, 91);
            this.m_DirectoryTextBox.Name = "m_DirectoryTextBox";
            this.m_DirectoryTextBox.Size = new System.Drawing.Size(229, 20);
            this.m_DirectoryTextBox.TabIndex = 1;
            this.m_DirectoryTextBox.Text = "Schedule Path";
            this.m_DirectoryTextBox.TextChanged += new System.EventHandler(this.m_DirectoryTextBox_TextChanged);
            // 
            // m_SalaryTab
            // 
            this.m_SalaryTab.Location = new System.Drawing.Point(4, 22);
            this.m_SalaryTab.Name = "m_SalaryTab";
            this.m_SalaryTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_SalaryTab.Size = new System.Drawing.Size(250, 206);
            this.m_SalaryTab.TabIndex = 1;
            this.m_SalaryTab.Text = "Salary";
            this.m_SalaryTab.UseVisualStyleBackColor = true;
            // 
            // m_OpenFileDialog
            // 
            this.m_OpenFileDialog.FileName = "openFileDialog1";
            this.m_OpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // m_NameTextBox
            // 
            this.m_NameTextBox.Location = new System.Drawing.Point(15, 7);
            this.m_NameTextBox.Name = "m_NameTextBox";
            this.m_NameTextBox.Size = new System.Drawing.Size(100, 20);
            this.m_NameTextBox.TabIndex = 3;
            this.m_NameTextBox.Text = "Name";
            this.m_NameTextBox.TextChanged += new System.EventHandler(this.m_NameTextBox_TextChanged);
            // 
            // m_SalaryTextBox
            // 
            this.m_SalaryTextBox.Location = new System.Drawing.Point(15, 33);
            this.m_SalaryTextBox.Name = "m_SalaryTextBox";
            this.m_SalaryTextBox.Size = new System.Drawing.Size(99, 20);
            this.m_SalaryTextBox.TabIndex = 4;
            this.m_SalaryTextBox.Text = "Salary";
            this.m_SalaryTextBox.TextChanged += new System.EventHandler(this.m_SalaryTextBox_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 223);
            this.Controls.Add(this.m_TabControl);
            this.Name = "MainForm";
            this.Text = "StenaThingamabob - Working Title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.m_TabControl.ResumeLayout(false);
            this.m_StartTab.ResumeLayout(false);
            this.m_StartTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_CalculateButton;
        private System.Windows.Forms.TabControl m_TabControl;
        private System.Windows.Forms.TabPage m_StartTab;
        private System.Windows.Forms.TabPage m_SalaryTab;
        private System.Windows.Forms.TextBox m_DirectoryTextBox;
        private System.Windows.Forms.Button m_BrowseButton;
        private System.Windows.Forms.OpenFileDialog m_OpenFileDialog;
        private System.Windows.Forms.TextBox m_NameTextBox;
        private System.Windows.Forms.TextBox m_SalaryTextBox;
    }
}

