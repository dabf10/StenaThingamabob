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
        ScheduleManager m_ScheduleManager = null;
        Calculator m_Calculator = null;
        ConfigManager m_ConfigManager = null;
        
        decimal m_SelectedWeekFrom = 1;
        decimal m_SelectedWeekTo = 1;

        public MainForm()
        {
            //Do basic windows forms initialization
            InitializeComponent();

            //Initialize default values for form members
            Initialize();

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

        private void Initialize()
        {
            m_ScheduleManager = new ScheduleManager();
            m_Calculator = new Calculator();
            m_ConfigManager = new ConfigManager(m_NameTextBox, m_DirectoryTextBox, m_SalaryTextBox, m_YearTextBox);
            m_ConfigManager.Initialize(ref m_SelectedWeekFrom, ref m_SelectedWeekTo);

            ToggleTabs(false);
        }

        /// <summary>
        /// Removes characters to the text according to the inputed boolean parameters
        /// </summary>
        /// <param name="input">The string to be sanitized</param>
        /// <param name="noNumbers">If true all numerical characters will be removed</param>
        /// <param name="noSpecialCharacters">If true all special characters will be removed(all none numerical or alpabetical characters)</param>
        /// <param name="noAlphabeticalCharacters">If true all alpabetical characters will be removed</param>
        /// <returns>The sanitized string</returns>
        private string SanteziseInput(string input, bool noNumbers = false, bool noSpecialCharacters = false, bool noAlphabeticalCharacters = false)
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
                    if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == 'å' || c == 'ä' || c == 'ö' || c == 'Å' || c == 'Ä' || c == 'Ö')
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
            else if (!m_ScheduleManager.YearExists(GetYear()))
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

        private void VerifySchedule()
        {
            if (!m_ScheduleManager.ScheduleLoaded())
                m_ScheduleManager.LoadSchedule(m_DirectoryTextBox.Text, Convert.ToUInt32(m_YearTextBox.Text));
            if (!m_ScheduleManager.WeeksLoaded(m_NameTextBox.Text, Convert.ToUInt32(m_YearTextBox.Text)))
                m_ScheduleManager.LoadWeeks(m_NameTextBox.Text, Convert.ToUInt32(m_YearTextBox.Text));
        }

        private bool LoadSchedule()
        {
            if (!m_ScheduleManager.ScheduleLoaded(m_DirectoryTextBox.Text))
            {
                if (!m_ScheduleManager.LoadSchedule(m_DirectoryTextBox.Text, GetYear()))
                {
                    m_MessageLabel.Text = "Loading failed - Please verify schedule file directory and year";
                    return false;
                }
            }

            if (!m_ScheduleManager.WeeksLoaded(m_NameTextBox.Text, GetYear()))
            {
                if (m_ScheduleManager.LoadWeeks(m_NameTextBox.Text, GetYear()))
                {
                    ToggleTabs(true);
                    return true;
                }
                else
                {
                    m_MessageLabel.Text = "Loading failed - Please verify name and year";
                    return false;
                }
            }
            else
                m_MessageLabel.Text = "Schedule already loaded for " + m_NameTextBox.Text + " " + m_YearTextBox.Text;
            return false;
        }

        private uint GetYear()
        {
            if (m_YearTextBox.Text != "" && m_YearTextBox.Text != null)
                return Convert.ToUInt32(m_YearTextBox.Text);
            else
                return 0;
        }

        private void ToggleTabs(bool tabsEnabled)
        {
            if (!tabsEnabled)
            {
                ((Control)m_SalaryTab).Enabled = false;
                ((Control)m_HoursTab).Enabled = false;
            }
            else
            {
                ((Control)m_SalaryTab).Enabled = true;
                ((Control)m_HoursTab).Enabled = true;
            }
        }

        private void InputInvalidated()
        {
            ToggleTabs(false);
            m_ScheduleManager.UnloadWeeks();
            m_TotalHoursDisplay.Text = "0";
            m_600PartsDisplay.Text = "0";
            m_400PartsDisplay.Text = "0";
            m_300PartsDisplay.Text = "0";
        }

        //***EVENT HANDLERS***

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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           m_ConfigManager.SaveConfig(ref m_SelectedWeekFrom, ref m_SelectedWeekTo);
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

            List<UtilityData.Week> weeksToCalculate = m_ScheduleManager.GetWeeks((uint)m_SelectedWeekFrom, (uint)m_SelectedWeekTo);
            UtilityData.WorkingHours calculatedWeekTotalHours = m_Calculator.CalculateMultipleWeeks(weeksToCalculate);

            m_TotalHoursDisplay.Text = calculatedWeekTotalHours.Base.ToString();
            m_600PartsDisplay.Text = calculatedWeekTotalHours.Part600.ToString();
            m_400PartsDisplay.Text = calculatedWeekTotalHours.Part400.ToString();
            m_300PartsDisplay.Text = calculatedWeekTotalHours.Part300.ToString();
        }

        private void m_NameTextBox_TextChanged(object sender, EventArgs e)
        {
            m_NameTextBox.Text = SanteziseInput(m_NameTextBox.Text, true, true, false); //Numbers and special characters are not allowed
            m_NameTextBox.Select(m_NameTextBox.Text.Length, 0); //To stop the marker from being placed to the left of the text when an invalid character is inputed.
            InputInvalidated();

            if (m_NameTextBox.Text == "hest" || m_NameTextBox.Text == "Hest" || m_NameTextBox.Text == "HEST")
                m_MessageLabel.Text = "gneg";
        }

        private void m_SalaryTextBox_TextChanged(object sender, EventArgs e)
        {
            m_SalaryTextBox.Text = SanteziseInput(m_SalaryTextBox.Text, false, true, true); //Only numbers are allowed
            m_SalaryTextBox.Select(m_SalaryTextBox.Text.Length, 0); //To stop the marker from being placed to the left of the text when an invalid character is inputed
        }

        private void m_YearTextBox_TextChanged(object sender, EventArgs e)
        {
            m_YearTextBox.Text = SanteziseInput(m_YearTextBox.Text, false, true, true);
            m_YearTextBox.Select(m_YearTextBox.Text.Length, 0);
            InputInvalidated();
        }

        private void m_LoadButton_Click(object sender, EventArgs e)
        {
            if (VerifyUserInput())
            {
                if (LoadSchedule())
                    m_MessageLabel.Text = "Loaded schedule for " + m_NameTextBox.Text + " " + m_YearTextBox.Text;
            }
            else
            {
                ToggleTabs(false);
                m_ScheduleManager.UnloadWeeks();
            }
        }

        private void m_DirectoryTextBox_TextChanged(object sender, EventArgs e)
        {
            m_ScheduleManager.UnloadSchedule();
            InputInvalidated();
        }
    }
}