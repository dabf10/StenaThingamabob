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
            this.m_TabControl = new System.Windows.Forms.TabControl();
            this.m_StartTab = new System.Windows.Forms.TabPage();
            this.m_LoadButton = new System.Windows.Forms.Button();
            this.m_YearTextBox = new System.Windows.Forms.TextBox();
            this.m_YearLabel = new System.Windows.Forms.Label();
            this.m_SchedulePathLabel = new System.Windows.Forms.Label();
            this.m_NameLabel = new System.Windows.Forms.Label();
            this.m_NameTextBox = new System.Windows.Forms.TextBox();
            this.m_BrowseButton = new System.Windows.Forms.Button();
            this.m_DirectoryTextBox = new System.Windows.Forms.TextBox();
            this.m_SalaryTab = new System.Windows.Forms.TabPage();
            this.m_SalaryLabel = new System.Windows.Forms.Label();
            this.m_SalarySelectWeekLabel = new System.Windows.Forms.Label();
            this.m_SalaryWeekControlTo = new System.Windows.Forms.NumericUpDown();
            this.m_SalaryWeekControlFrom = new System.Windows.Forms.NumericUpDown();
            this.m_CalculateSalaryButton = new System.Windows.Forms.Button();
            this.m_SalaryTextBox = new System.Windows.Forms.TextBox();
            this.m_HoursTab = new System.Windows.Forms.TabPage();
            this.m_TotalHoursDisplay = new System.Windows.Forms.Label();
            this.m_TotalHoursLabel = new System.Windows.Forms.Label();
            this.m_CalculateHoursButton = new System.Windows.Forms.Button();
            this.m_HoursWeekControlTo = new System.Windows.Forms.NumericUpDown();
            this.m_HoursWeekControlFrom = new System.Windows.Forms.NumericUpDown();
            this.m_HoursSelectWeekLabel = new System.Windows.Forms.Label();
            this.m_OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_MessageLabel = new System.Windows.Forms.Label();
            this.m_TabControl.SuspendLayout();
            this.m_StartTab.SuspendLayout();
            this.m_SalaryTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_SalaryWeekControlTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_SalaryWeekControlFrom)).BeginInit();
            this.m_HoursTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_HoursWeekControlTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_HoursWeekControlFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // m_TabControl
            // 
            this.m_TabControl.Controls.Add(this.m_StartTab);
            this.m_TabControl.Controls.Add(this.m_SalaryTab);
            this.m_TabControl.Controls.Add(this.m_HoursTab);
            this.m_TabControl.Location = new System.Drawing.Point(-6, -4);
            this.m_TabControl.Name = "m_TabControl";
            this.m_TabControl.SelectedIndex = 0;
            this.m_TabControl.Size = new System.Drawing.Size(258, 232);
            this.m_TabControl.TabIndex = 1;
            // 
            // m_StartTab
            // 
            this.m_StartTab.Controls.Add(this.m_LoadButton);
            this.m_StartTab.Controls.Add(this.m_YearTextBox);
            this.m_StartTab.Controls.Add(this.m_YearLabel);
            this.m_StartTab.Controls.Add(this.m_SchedulePathLabel);
            this.m_StartTab.Controls.Add(this.m_NameLabel);
            this.m_StartTab.Controls.Add(this.m_NameTextBox);
            this.m_StartTab.Controls.Add(this.m_BrowseButton);
            this.m_StartTab.Controls.Add(this.m_DirectoryTextBox);
            this.m_StartTab.Location = new System.Drawing.Point(4, 22);
            this.m_StartTab.Name = "m_StartTab";
            this.m_StartTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_StartTab.Size = new System.Drawing.Size(250, 206);
            this.m_StartTab.TabIndex = 0;
            this.m_StartTab.Text = "Start";
            this.m_StartTab.UseVisualStyleBackColor = true;
            // 
            // m_LoadButton
            // 
            this.m_LoadButton.Location = new System.Drawing.Point(70, 130);
            this.m_LoadButton.Name = "m_LoadButton";
            this.m_LoadButton.Size = new System.Drawing.Size(75, 52);
            this.m_LoadButton.TabIndex = 8;
            this.m_LoadButton.Text = "Load";
            this.m_LoadButton.UseVisualStyleBackColor = true;
            this.m_LoadButton.Click += new System.EventHandler(this.m_LoadButton_Click);
            // 
            // m_YearTextBox
            // 
            this.m_YearTextBox.Location = new System.Drawing.Point(55, 33);
            this.m_YearTextBox.Name = "m_YearTextBox";
            this.m_YearTextBox.Size = new System.Drawing.Size(100, 20);
            this.m_YearTextBox.TabIndex = 6;
            this.m_YearTextBox.TextChanged += new System.EventHandler(this.m_YearTextBox_TextChanged);
            // 
            // m_YearLabel
            // 
            this.m_YearLabel.AutoSize = true;
            this.m_YearLabel.Location = new System.Drawing.Point(14, 36);
            this.m_YearLabel.Name = "m_YearLabel";
            this.m_YearLabel.Size = new System.Drawing.Size(29, 13);
            this.m_YearLabel.TabIndex = 7;
            this.m_YearLabel.Text = "Year";
            // 
            // m_SchedulePathLabel
            // 
            this.m_SchedulePathLabel.AutoSize = true;
            this.m_SchedulePathLabel.Location = new System.Drawing.Point(68, 66);
            this.m_SchedulePathLabel.Name = "m_SchedulePathLabel";
            this.m_SchedulePathLabel.Size = new System.Drawing.Size(77, 13);
            this.m_SchedulePathLabel.TabIndex = 5;
            this.m_SchedulePathLabel.Text = "Schedule Path";
            // 
            // m_NameLabel
            // 
            this.m_NameLabel.Location = new System.Drawing.Point(14, 6);
            this.m_NameLabel.Name = "m_NameLabel";
            this.m_NameLabel.Size = new System.Drawing.Size(35, 20);
            this.m_NameLabel.TabIndex = 4;
            this.m_NameLabel.Text = "Name";
            this.m_NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_NameTextBox
            // 
            this.m_NameTextBox.Location = new System.Drawing.Point(55, 7);
            this.m_NameTextBox.Name = "m_NameTextBox";
            this.m_NameTextBox.Size = new System.Drawing.Size(100, 20);
            this.m_NameTextBox.TabIndex = 3;
            this.m_NameTextBox.TextChanged += new System.EventHandler(this.m_NameTextBox_TextChanged);
            // 
            // m_BrowseButton
            // 
            this.m_BrowseButton.Location = new System.Drawing.Point(209, 87);
            this.m_BrowseButton.Name = "m_BrowseButton";
            this.m_BrowseButton.Size = new System.Drawing.Size(26, 20);
            this.m_BrowseButton.TabIndex = 2;
            this.m_BrowseButton.Text = "...";
            this.m_BrowseButton.UseVisualStyleBackColor = true;
            this.m_BrowseButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // m_DirectoryTextBox
            // 
            this.m_DirectoryTextBox.Location = new System.Drawing.Point(14, 88);
            this.m_DirectoryTextBox.Name = "m_DirectoryTextBox";
            this.m_DirectoryTextBox.Size = new System.Drawing.Size(190, 20);
            this.m_DirectoryTextBox.TabIndex = 1;
            this.m_DirectoryTextBox.TextChanged += new System.EventHandler(this.m_DirectoryTextBox_TextChanged);
            // 
            // m_SalaryTab
            // 
            this.m_SalaryTab.Controls.Add(this.m_SalaryLabel);
            this.m_SalaryTab.Controls.Add(this.m_SalarySelectWeekLabel);
            this.m_SalaryTab.Controls.Add(this.m_SalaryWeekControlTo);
            this.m_SalaryTab.Controls.Add(this.m_SalaryWeekControlFrom);
            this.m_SalaryTab.Controls.Add(this.m_CalculateSalaryButton);
            this.m_SalaryTab.Controls.Add(this.m_SalaryTextBox);
            this.m_SalaryTab.Location = new System.Drawing.Point(4, 22);
            this.m_SalaryTab.Name = "m_SalaryTab";
            this.m_SalaryTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_SalaryTab.Size = new System.Drawing.Size(250, 206);
            this.m_SalaryTab.TabIndex = 1;
            this.m_SalaryTab.Text = "Salary";
            this.m_SalaryTab.UseVisualStyleBackColor = true;
            this.m_SalaryTab.Enter += new System.EventHandler(this.m_SalaryTab_Enter);
            // 
            // m_SalaryLabel
            // 
            this.m_SalaryLabel.AutoSize = true;
            this.m_SalaryLabel.Location = new System.Drawing.Point(106, 11);
            this.m_SalaryLabel.Name = "m_SalaryLabel";
            this.m_SalaryLabel.Size = new System.Drawing.Size(36, 13);
            this.m_SalaryLabel.TabIndex = 10;
            this.m_SalaryLabel.Text = "Salary";
            // 
            // m_SalarySelectWeekLabel
            // 
            this.m_SalarySelectWeekLabel.Location = new System.Drawing.Point(88, 65);
            this.m_SalarySelectWeekLabel.Name = "m_SalarySelectWeekLabel";
            this.m_SalarySelectWeekLabel.Size = new System.Drawing.Size(75, 13);
            this.m_SalarySelectWeekLabel.TabIndex = 9;
            this.m_SalarySelectWeekLabel.Text = "Select weeks";
            this.m_SalarySelectWeekLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_SalarySelectWeekLabel.UseCompatibleTextRendering = true;
            // 
            // m_SalaryWeekControlTo
            // 
            this.m_SalaryWeekControlTo.Location = new System.Drawing.Point(138, 81);
            this.m_SalaryWeekControlTo.Maximum = new decimal(new int[] {
            52,
            0,
            0,
            0});
            this.m_SalaryWeekControlTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_SalaryWeekControlTo.Name = "m_SalaryWeekControlTo";
            this.m_SalaryWeekControlTo.Size = new System.Drawing.Size(38, 20);
            this.m_SalaryWeekControlTo.TabIndex = 8;
            this.m_SalaryWeekControlTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_SalaryWeekControlTo.ValueChanged += new System.EventHandler(this.m_SalaryWeekControlTo_ValueChanged);
            // 
            // m_SalaryWeekControlFrom
            // 
            this.m_SalaryWeekControlFrom.Location = new System.Drawing.Point(77, 81);
            this.m_SalaryWeekControlFrom.Maximum = new decimal(new int[] {
            52,
            0,
            0,
            0});
            this.m_SalaryWeekControlFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_SalaryWeekControlFrom.Name = "m_SalaryWeekControlFrom";
            this.m_SalaryWeekControlFrom.Size = new System.Drawing.Size(38, 20);
            this.m_SalaryWeekControlFrom.TabIndex = 7;
            this.m_SalaryWeekControlFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_SalaryWeekControlFrom.ValueChanged += new System.EventHandler(this.m_SalaryWeekControlFrom_ValueChanged);
            // 
            // m_CalculateSalaryButton
            // 
            this.m_CalculateSalaryButton.Location = new System.Drawing.Point(77, 124);
            this.m_CalculateSalaryButton.Name = "m_CalculateSalaryButton";
            this.m_CalculateSalaryButton.Size = new System.Drawing.Size(99, 23);
            this.m_CalculateSalaryButton.TabIndex = 6;
            this.m_CalculateSalaryButton.Text = "Calculate";
            this.m_CalculateSalaryButton.UseVisualStyleBackColor = true;
            // 
            // m_SalaryTextBox
            // 
            this.m_SalaryTextBox.Location = new System.Drawing.Point(77, 27);
            this.m_SalaryTextBox.Name = "m_SalaryTextBox";
            this.m_SalaryTextBox.Size = new System.Drawing.Size(99, 20);
            this.m_SalaryTextBox.TabIndex = 5;
            // 
            // m_HoursTab
            // 
            this.m_HoursTab.Controls.Add(this.m_TotalHoursDisplay);
            this.m_HoursTab.Controls.Add(this.m_TotalHoursLabel);
            this.m_HoursTab.Controls.Add(this.m_CalculateHoursButton);
            this.m_HoursTab.Controls.Add(this.m_HoursWeekControlTo);
            this.m_HoursTab.Controls.Add(this.m_HoursWeekControlFrom);
            this.m_HoursTab.Controls.Add(this.m_HoursSelectWeekLabel);
            this.m_HoursTab.Location = new System.Drawing.Point(4, 22);
            this.m_HoursTab.Name = "m_HoursTab";
            this.m_HoursTab.Size = new System.Drawing.Size(250, 206);
            this.m_HoursTab.TabIndex = 2;
            this.m_HoursTab.Text = "Hours";
            this.m_HoursTab.UseVisualStyleBackColor = true;
            this.m_HoursTab.Enter += new System.EventHandler(this.m_HoursPage_Enter);
            // 
            // m_TotalHoursDisplay
            // 
            this.m_TotalHoursDisplay.AutoSize = true;
            this.m_TotalHoursDisplay.Location = new System.Drawing.Point(165, 105);
            this.m_TotalHoursDisplay.Name = "m_TotalHoursDisplay";
            this.m_TotalHoursDisplay.Size = new System.Drawing.Size(13, 13);
            this.m_TotalHoursDisplay.TabIndex = 15;
            this.m_TotalHoursDisplay.Text = "0";
            // 
            // m_TotalHoursLabel
            // 
            this.m_TotalHoursLabel.AutoSize = true;
            this.m_TotalHoursLabel.Location = new System.Drawing.Point(53, 105);
            this.m_TotalHoursLabel.Name = "m_TotalHoursLabel";
            this.m_TotalHoursLabel.Size = new System.Drawing.Size(35, 13);
            this.m_TotalHoursLabel.TabIndex = 14;
            this.m_TotalHoursLabel.Text = "Hours";
            // 
            // m_CalculateHoursButton
            // 
            this.m_CalculateHoursButton.Location = new System.Drawing.Point(90, 71);
            this.m_CalculateHoursButton.Name = "m_CalculateHoursButton";
            this.m_CalculateHoursButton.Size = new System.Drawing.Size(75, 23);
            this.m_CalculateHoursButton.TabIndex = 13;
            this.m_CalculateHoursButton.Text = "Calculate";
            this.m_CalculateHoursButton.UseVisualStyleBackColor = true;
            this.m_CalculateHoursButton.Click += new System.EventHandler(this.m_CalculateHoursButton_Click);
            // 
            // m_HoursWeekControlTo
            // 
            this.m_HoursWeekControlTo.Location = new System.Drawing.Point(163, 30);
            this.m_HoursWeekControlTo.Maximum = new decimal(new int[] {
            52,
            0,
            0,
            0});
            this.m_HoursWeekControlTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_HoursWeekControlTo.Name = "m_HoursWeekControlTo";
            this.m_HoursWeekControlTo.Size = new System.Drawing.Size(38, 20);
            this.m_HoursWeekControlTo.TabIndex = 12;
            this.m_HoursWeekControlTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_HoursWeekControlTo.ValueChanged += new System.EventHandler(this.m_HoursWeekControlTo_ValueChanged);
            // 
            // m_HoursWeekControlFrom
            // 
            this.m_HoursWeekControlFrom.Location = new System.Drawing.Point(56, 30);
            this.m_HoursWeekControlFrom.Maximum = new decimal(new int[] {
            52,
            0,
            0,
            0});
            this.m_HoursWeekControlFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_HoursWeekControlFrom.Name = "m_HoursWeekControlFrom";
            this.m_HoursWeekControlFrom.Size = new System.Drawing.Size(38, 20);
            this.m_HoursWeekControlFrom.TabIndex = 11;
            this.m_HoursWeekControlFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_HoursWeekControlFrom.ValueChanged += new System.EventHandler(this.m_HoursWeekControlFrom_ValueChanged);
            // 
            // m_HoursSelectWeekLabel
            // 
            this.m_HoursSelectWeekLabel.Location = new System.Drawing.Point(90, 17);
            this.m_HoursSelectWeekLabel.Name = "m_HoursSelectWeekLabel";
            this.m_HoursSelectWeekLabel.Size = new System.Drawing.Size(75, 10);
            this.m_HoursSelectWeekLabel.TabIndex = 10;
            this.m_HoursSelectWeekLabel.Text = "Select weeks";
            this.m_HoursSelectWeekLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_HoursSelectWeekLabel.UseCompatibleTextRendering = true;
            // 
            // m_OpenFileDialog
            // 
            this.m_OpenFileDialog.FileName = "openFileDialog1";
            // 
            // m_MessageLabel
            // 
            this.m_MessageLabel.BackColor = System.Drawing.SystemColors.Window;
            this.m_MessageLabel.Location = new System.Drawing.Point(-2, 227);
            this.m_MessageLabel.Name = "m_MessageLabel";
            this.m_MessageLabel.Size = new System.Drawing.Size(250, 27);
            this.m_MessageLabel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 246);
            this.Controls.Add(this.m_MessageLabel);
            this.Controls.Add(this.m_TabControl);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(261, 284);
            this.MinimumSize = new System.Drawing.Size(261, 284);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "StenaThingamabob - Working Title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.m_TabControl.ResumeLayout(false);
            this.m_StartTab.ResumeLayout(false);
            this.m_StartTab.PerformLayout();
            this.m_SalaryTab.ResumeLayout(false);
            this.m_SalaryTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_SalaryWeekControlTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_SalaryWeekControlFrom)).EndInit();
            this.m_HoursTab.ResumeLayout(false);
            this.m_HoursTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_HoursWeekControlTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_HoursWeekControlFrom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl m_TabControl;
        private System.Windows.Forms.TabPage m_StartTab;
        private System.Windows.Forms.TabPage m_SalaryTab;
        private System.Windows.Forms.TextBox m_DirectoryTextBox;
        private System.Windows.Forms.Button m_BrowseButton;
        private System.Windows.Forms.OpenFileDialog m_OpenFileDialog;
        private System.Windows.Forms.TextBox m_NameTextBox;
        private System.Windows.Forms.Label m_SalarySelectWeekLabel;
        private System.Windows.Forms.NumericUpDown m_SalaryWeekControlTo;
        private System.Windows.Forms.NumericUpDown m_SalaryWeekControlFrom;
        private System.Windows.Forms.Button m_CalculateSalaryButton;
        private System.Windows.Forms.TextBox m_SalaryTextBox;
        private System.Windows.Forms.Label m_SalaryLabel;
        private System.Windows.Forms.Label m_NameLabel;
        private System.Windows.Forms.Label m_SchedulePathLabel;
        private System.Windows.Forms.Button m_CalculateHoursButton;
        private System.Windows.Forms.NumericUpDown m_HoursWeekControlTo;
        private System.Windows.Forms.NumericUpDown m_HoursWeekControlFrom;
        private System.Windows.Forms.Label m_HoursSelectWeekLabel;
        private System.Windows.Forms.Label m_TotalHoursDisplay;
        private System.Windows.Forms.Label m_TotalHoursLabel;
        private System.Windows.Forms.Label m_YearLabel;
        private System.Windows.Forms.TextBox m_YearTextBox;
        private System.Windows.Forms.Label m_MessageLabel;
        private System.Windows.Forms.Button m_LoadButton;
        public System.Windows.Forms.TabPage m_HoursTab;
    }
}

