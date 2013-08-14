using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace StenaThingamabob___Working_Title
{
    public partial class MainForm : Form
    {
        ScheduleManager m_ScheduleManager = new ScheduleManager();
        Calculator m_Calculator = new Calculator();

        decimal m_SelectedWeekFrom = 1;
        decimal m_SelectedWeekTo = 1;

        public MainForm()
        {
            //Do basic windows forms initialization
            InitializeComponent();

            //Initialize default values for form members
            InitializeValues();

            // Load the config file. Create it if it doesn't exist
            if (!ConfigFileExists())
                CreateConfigFile();
            LoadConfig();

            //Try to load the schedule from the config file settings
            if (VerifyUserInput())
            {
                if (LoadSchedule())
                    m_MessageLabel.Text = "Schedule loaded from config";
                else
                    m_MessageLabel.ResetText();
            }
            else
                m_MessageLabel.ResetText();
        }

        private void InitializeValues()
        {
            ((Control)m_SalaryTab).Enabled = false;
            ((Control)m_HoursTab).Enabled = false;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Opens a file browser for the user to select a file and writes the path to DirectoryPathTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = m_OpenFileDialog.ShowDialog();
            string path = m_OpenFileDialog.FileName;
            if (path != "openFileDialog1") //Early escape if the cancel button was pressed
            {
                if (result == DialogResult.OK)
                {
                    string extension = Path.GetExtension(path);
                    if (extension == ".xlsx" || extension == ".xls")
                        m_DirectoryTextBox.Text = m_OpenFileDialog.FileName;
                    else
                        MessageBox.Show("ERROR - Invlid file extension. Please select a file with .xls or .xlsx extension");
                }
                else
                    m_MessageLabel.Text = "Given schedule path is invalid";
            }
        }

        private void m_DirectoryTextBox_TextChanged(object sender, EventArgs e) //This input should not be sanitized
        {

        }        

        /// <summary>
        /// Checks if the config file is already created
        /// </summary>
        /// <returns>True if the file exists</returns>
        bool ConfigFileExists()
        {
            if (File.Exists(UtilityData.FilePaths.ConfigPath))
                return true;
            return false;
        }

        /// <summary>
        /// Creates the config file and assigns its default values
        /// </summary>
        void CreateConfigFile()
        {
            if (!File.Exists(UtilityData.FilePaths.ConfigPath))
            {
                StreamWriter writer = new StreamWriter(UtilityData.FilePaths.ConfigPath);

                writer.WriteLine(UtilityData.ConfigProperties.directory + '=');
                writer.WriteLine(UtilityData.ConfigProperties.name + '=');
                writer.WriteLine(UtilityData.ConfigProperties.salary + '=');
                writer.WriteLine(UtilityData.ConfigProperties.year + '=');

                writer.Dispose();
            }
        }

        /// <summary>
        /// Reads the value of the inputed property in the config file
        /// </summary>
        /// <param name="propertyName"> The name of the property from wich to read the value</param>
        /// <returns>The value of the inputed property as a string. Returns empty string if the property was not found or was not set.</returns>
        string ReadFromConfig(string propertyName)
        {
            if (File.Exists(UtilityData.FilePaths.ConfigPath))
            {
                StreamReader reader = new StreamReader(UtilityData.FilePaths.ConfigPath);
                string toReturn = string.Empty;

                string line = string.Empty;
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        continue;

                    string[] splitLine = line.Split('=');

                    if (splitLine[0] == propertyName)
                    {
                        toReturn = splitLine[1];
                        break; //Further iterations are irrelevant
                    }
                }
                reader.Dispose();
                return toReturn;
            }
            return string.Empty;
        }


        //Disabled until problem with overwriting in txt file has been resolved
        ///// <summary>
        ///// Writes the inputed value to the inputed property in the config file
        ///// </summary>
        ///// <param name="propertyName">The name of the property wich to write the value to</param>
        ///// <param name="value">The value to be written to the inputed property</param>
        //void WriteToConfig(string propertyName, string value)
        //{
        //    StreamReader reader = new StreamReader(Application.StartupPath + "config.txt");

        //    string line = "";
        //    while (line != null)
        //    {
        //        line = reader.ReadLine().Split('=')[0]; //Only the part before the '=' should be comp
        //        if (line == propertyName)
        //        {
        //            reader.Dispose();
        //            StreamWriter writer = new StreamWriter(Application.StartupPath + "Config.txt");
        //            writer.WriteLine(propertyName + "=" + value);
        //            writer.Dispose();
        //            return; //We're done here!
        //        }
        //    }
        //}


        /// <summary>
        /// Loads the config file
        /// </summary>
        void LoadConfig()
        {
            if (File.Exists(UtilityData.FilePaths.ConfigPath))
            {
                StreamReader reader = new StreamReader(UtilityData.FilePaths.ConfigPath);
                string readString = "";

                readString = ReadFromConfig(UtilityData.ConfigProperties.directory);
                if (readString != "" && readString != null)
                    m_DirectoryTextBox.Text = readString;
                else
                    Console.WriteLine("Could not read SchedulePath from config. Is it empty?");

                readString = ReadFromConfig(UtilityData.ConfigProperties.name);
                if (readString != "" && readString != null)
                    m_NameTextBox.Text = readString;
                else
                    Console.WriteLine("Could not read Name from config. Is it empty?");

                readString = ReadFromConfig(UtilityData.ConfigProperties.salary);
                if (readString != "" && readString != null)
                    m_SalaryTextBox.Text = readString;
                else
                    Console.WriteLine("Could not read Salary from config. Is it empty?");

                readString = ReadFromConfig(UtilityData.ConfigProperties.year);
                if (readString != "" && readString != null)
                    m_YearTextBox.Text = readString;

                reader.Dispose();
            }
        }

        private void m_NameTextBox_TextChanged(object sender, EventArgs e)
        {
            m_NameTextBox.Text = SanteziseInput(m_NameTextBox.Text, true, true, false); //Numbers and special characters are not allowed
            m_NameTextBox.Select(m_NameTextBox.Text.Length, 0); //To stop the marker from being placed to the left of the text when an invalid character is inputed.
        }

        /// <summary>
        /// Removes characters to the text according to the inputed boolean parameters
        /// </summary>
        /// <param name="input">The string to be sanitized</param>
        /// <param name="noNumbers">If true all numerical characters will be removed</param>
        /// <param name="noSpecialCharacters">If true all special characters will be removed(all none numerical or alpabetical characters)</param>
        /// <param name="noAlphabeticalCharacters">If true all alpabetical characters will be removed</param>
        /// <returns>The sanitized string</returns>
        string SanteziseInput(string input, bool noNumbers = false, bool noSpecialCharacters = false, bool noAlphabeticalCharacters = false)
        {
            StringBuilder builder = new StringBuilder();
            
            foreach (char c in input)
            {
                if (noSpecialCharacters && noAlphabeticalCharacters)
                {
                    if (c >= '0' && c <= '9')
                        builder.Append(c);
                }

                else if (noNumbers && noAlphabeticalCharacters)
                {
                    if (((c < 'A' && c > 'Z') && (c < 'a' && c > 'z') && (c < '0' && c > '9')))
                        builder.Append(c); //TODO - test
                }

                else if (noNumbers && noSpecialCharacters)
                {
                    if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ') // todo - allow åäö
                        builder.Append(c);
                }

                else if (noNumbers)
                {
                    if (c < '0' && c > '9')
                        builder.Append(c);
                }

                else if (noSpecialCharacters)
                {
                    if (((c >= 'A' && c <= 'Z') && (c >= 'a' && c <= 'z')) || c >= '0' && c <= '9')
                        builder.Append(c);
                }

                else if (noAlphabeticalCharacters)
                {
                    if ((c < 'A' && c > 'Z') && (c < 'a' && c > 'z'))
                        builder.Append(c); //TODO - test
                }
            }
            return builder.ToString();
        }

        private void m_SalaryTextBox_TextChanged(object sender, EventArgs e)
        {
            m_SalaryTextBox.Text = SanteziseInput(m_SalaryTextBox.Text, false, true, true); //Only numbers are allowed
            m_SalaryTextBox.Select(m_SalaryTextBox.Text.Length, 0); //To stop the marker from being placed to the left of the text when an invalid character is inputed
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAllToConfig();
        }

        /// <summary>
        /// Saves all config properties to the config file
        /// </summary>
        private void SaveAllToConfig()
        {
            if (File.Exists(UtilityData.FilePaths.ConfigPath))
            {
                StreamWriter writer = new StreamWriter(UtilityData.FilePaths.ConfigPath);

                //Directory
                if (m_DirectoryTextBox.Text != "" && m_DirectoryTextBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.directory + '=' + m_DirectoryTextBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.directory + '=');

                //Name
                if (m_NameTextBox.Text != "" && m_NameTextBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.name + '=' + m_NameTextBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.name + '=');

                //Salary
                if (m_SalaryTextBox.Text != "" && m_SalaryTextBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.salary + '=' + m_SalaryTextBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.salary + '=');

                if (m_YearTextBox.Text != "" && m_YearTextBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.year + '=' + m_YearTextBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.year + '=');

                writer.Dispose();
            }
        }

        private void m_SalaryWeekControlTo_ValueChanged(object sender, EventArgs e)
        {
            VerifySchedule();
            m_SelectedWeekTo = m_SalaryWeekControlTo.Value;
            if (m_SelectedWeekTo < m_SelectedWeekFrom)
                m_SalaryWeekControlFrom.Value = m_SalaryWeekControlTo.Value;
            if (m_SelectedWeekTo > m_ScheduleManager.NumberOfWeeks())
                m_SalaryWeekControlTo.Value = m_ScheduleManager.NumberOfWeeks();
        }

        private void m_SalaryWeekControlFrom_ValueChanged(object sender, EventArgs e)
        {
            VerifySchedule();
            m_SelectedWeekFrom = m_SalaryWeekControlFrom.Value;
            if (m_SelectedWeekFrom > m_SelectedWeekTo)
                m_SalaryWeekControlTo.Value = m_SalaryWeekControlFrom.Value;
            if (m_SelectedWeekFrom < 1)
                m_SelectedWeekFrom = 1;
        }

        private void m_HoursWeekControlFrom_ValueChanged(object sender, EventArgs e)
        {
            VerifySchedule();
            m_SelectedWeekFrom = m_HoursWeekControlFrom.Value;
            if (m_SelectedWeekFrom > m_SelectedWeekTo)
                m_HoursWeekControlTo.Value = m_HoursWeekControlFrom.Value;
            if (m_SelectedWeekFrom < 1)
                m_SelectedWeekFrom = 1;
        }

        private void m_HoursWeekControlTo_ValueChanged(object sender, EventArgs e)
        {
            VerifySchedule();
            m_SelectedWeekTo = m_HoursWeekControlTo.Value;
            if (m_SelectedWeekTo < m_SelectedWeekFrom)
                m_HoursWeekControlFrom.Value = m_HoursWeekControlTo.Value;
            if (m_SelectedWeekTo > m_ScheduleManager.NumberOfWeeks())
                m_HoursWeekControlTo.Value = m_ScheduleManager.NumberOfWeeks();
        }

        private void m_SalaryTab_Enter(object sender, EventArgs e)
        {
            m_SalaryWeekControlFrom.Value = m_SelectedWeekFrom;
            m_SalaryWeekControlTo.Value = m_SelectedWeekTo;
        }

        private void m_HoursPage_Enter(object sender, EventArgs e)
        {
            m_HoursWeekControlFrom.Value = m_SelectedWeekFrom;
            m_HoursWeekControlTo.Value = m_SelectedWeekTo;
        }

        private void m_CalculateHoursButton_Click(object sender, EventArgs e)
        {
            if (!m_ScheduleManager.ScheduleLoaded())
                    m_ScheduleManager.LoadSchedule(m_DirectoryTextBox.Text, GetYear());
            if(!m_ScheduleManager.WeeksLoaded(m_NameTextBox.Text, Convert.ToUInt32(m_YearTextBox.Text)))
                m_ScheduleManager.LoadWeeks(m_NameTextBox.Text, Convert.ToUInt32(m_YearTextBox.Text));

            m_TotalHoursDisplay.Text = m_Calculator.CalculateMultipleWeeksTotalTime(m_ScheduleManager.GetWeeks((uint)m_SelectedWeekFrom, (uint)m_SelectedWeekTo)).ToString();
        }

        private void m_YearTextBox_TextChanged(object sender, EventArgs e)
        {
            m_YearTextBox.Text = SanteziseInput(m_YearTextBox.Text, false, true, true);
            m_YearTextBox.Select(m_YearTextBox.Text.Length, 0);
        }

        private void VerifySchedule()
        {
            if (!m_ScheduleManager.ScheduleLoaded())
                m_ScheduleManager.LoadSchedule(m_DirectoryTextBox.Text, Convert.ToUInt32(m_YearTextBox.Text));
            if (!m_ScheduleManager.WeeksLoaded(m_NameTextBox.Text, Convert.ToUInt32(m_YearTextBox.Text)))
                m_ScheduleManager.LoadWeeks(m_NameTextBox.Text, Convert.ToUInt32(m_YearTextBox.Text));
        }

        private void m_LoadButton_Click(object sender, EventArgs e)
        {
            if (VerifyUserInput())
            {
                if (LoadSchedule())
                    m_MessageLabel.Text = "Loaded schedule for " + m_NameTextBox.Text + " " + m_YearTextBox.Text;
                else
                {
                    m_MessageLabel.Text = "Loading failed - Please check input for errors";
                    ((Control)m_SalaryTab).Enabled = false;
                    ((Control)m_HoursTab).Enabled = false;
                    m_ScheduleManager.UnloadWeeks();
                }
            }
            else
            {
                ((Control)m_SalaryTab).Enabled = false;
                ((Control)m_HoursTab).Enabled = false;
                m_ScheduleManager.UnloadWeeks();
            }
        }

        private bool VerifyUserInput()
        {
            if (m_NameTextBox.Text == "" || m_NameTextBox.Text == null)
            {
                m_MessageLabel.Text = "Name cannot be empty";
                return false;
            }
            else if (m_YearTextBox.Text == "" || m_NameTextBox.Text == null)
            {
                m_MessageLabel.Text = "Year cannot be empty";
                return false;
            }
            else if(!m_ScheduleManager.YearExists(GetYear()))
            {
                m_MessageLabel.Text = "The given year could not be found";
                return false;
            }

            else if (m_DirectoryTextBox.Text == "" || m_DirectoryTextBox.Text == null)
            {
                m_MessageLabel.Text = "Path to schedule file needed";
                return false;
            }
            else
                return true;
        }

        private uint GetYear()
        {
            if (m_YearTextBox.Text != "" && m_YearTextBox.Text != null)
                return Convert.ToUInt32(m_YearTextBox.Text);
            else
                return 0;
        }

        private bool LoadSchedule()
        {
            if (m_ScheduleManager.LoadSchedule(m_DirectoryTextBox.Text, GetYear()))
            {
                if (m_ScheduleManager.LoadWeeks(m_NameTextBox.Text, GetYear()))
                {
                    ((Control)m_SalaryTab).Enabled = true;
                    ((Control)m_HoursTab).Enabled = true;
                    return true;
                }
                else
                {
                    m_MessageLabel.Text = "Loading failed - Please verify name and year";
                    return false;
                }
            }
            else
                m_MessageLabel.Text = "Loading failed - Please verify schedule file directory and year";
            return false;
        }
    }
}