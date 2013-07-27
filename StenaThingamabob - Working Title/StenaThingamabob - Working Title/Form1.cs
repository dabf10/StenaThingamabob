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
using System.Data.OleDb;

namespace StenaThingamabob___Working_Title
{
    public partial class MainForm : Form
    {
        Dictionary<string, DataTable> m_ScheduleData = new Dictionary<string, DataTable>(); //Key = sheetname Value = Loaded .xls sheet as DataTable
        uint m_NumberOfEmployees = 0; //The current number of employees on the schedule. Is read from Config.txt

        public MainForm()
        {
            //Do basic windows forms initialization
            InitializeComponent();

            // Load the config file. Create it if it doesn't exist
            if (!ConfigFileExists())
                CreateConfigFile();
            LoadConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsScheduleLoaded())
                LoadSchedule(m_DirectoryTextBox.Text);

            FindScheduleRowOnName(m_NameTextBox.Text);
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
                    MessageBox.Show("ERROR - Invalid Directory");
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// Tries to load the schedule from the inputed path using an OleDbConnection. If successfull the path is saved to "LastPath.txt".
        /// </summary>
        /// <param name="filePath">The path to the schedule file(.xls/.xlsx)</param>
        void LoadSchedule(string filePath)
        {
            string connectionString;

            //Validate file extension
            if (filePath.Trim().EndsWith(".xlsx"))
            {
                connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", filePath); //Imex = 1 means force read all values as text. HDR = YES means use header
            }
            else if (filePath.Trim().EndsWith(".xls"))
            {
                connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data Source={0}; Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1\";", filePath);
            }
            else
            {
                MessageBox.Show("ERROR - The inputed file path does not point to an .xlsx or .xls file");
                return;
            }

            //Initialize the database variables
            OleDbConnection connection = null;
            OleDbCommand command = null;
            OleDbDataAdapter adapter = null;

            DataTable sheetData = new DataTable();

            try
            {
                //Initialize the database connection
                connection = new OleDbConnection(connectionString);
                connection.Open();

                command = new OleDbCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;

                adapter = new OleDbDataAdapter(command);

                //Read the data
                string sheetName = "ve";
                string sheetNumber = null;
                for (int i = 0; i < 23; ++i)
                {
                    sheetNumber = (11 + i).ToString();
                    command.CommandText = @"SELECT * FROM [" + sheetName + sheetNumber + "$]";
                    adapter.Fill(sheetData);
                    m_ScheduleData.Add(sheetName + sheetNumber, sheetData);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Dispose();
                command.Dispose();
                adapter.Dispose();
            }
        }

        /// <summary>
        /// Checks if there are any entries in the schedule data dictionary
        /// </summary>
        /// <returns>True if schedule is loaded.</returns>
        bool IsScheduleLoaded()
        {
            if (m_ScheduleData.Count() > 0)
                return true;
            return false;
        }

        private void m_DirectoryTextBox_TextChanged(object sender, EventArgs e) //This input should not be sanitized
        {

        }

        /// <summary>
        /// Finds the cells belonging to a given name in the schedule
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <returns>List of strings containing the cell data</returns>
        List<string> FindScheduleRowOnName(string name)
        {
            int stenaLineLabelsFound = 0;
            string currentSheet = "ve11"; //Temp
            List<string> toReturn = new List<string>();

            string cellData = string.Empty;
            for (int column = 0; column < 25; ++column)
            {
                for (int row = 0; row < 100; ++row)
                {
                    cellData = m_ScheduleData[currentSheet].Rows[row][column].ToString();

                    if (stenaLineLabelsFound <= 0)
                    {
                        if (cellData == "Stena Line")
                            ++stenaLineLabelsFound;
                    }
                    else if (stenaLineLabelsFound == 1)
                    {
                        if (cellData == "Stena Line")
                            ++stenaLineLabelsFound;
                        else if (cellData == m_NameTextBox.Text)
                        {
                            for (int i = 1; i <= 18; ++i)
                            {
                                toReturn.Add(m_ScheduleData[currentSheet].Rows[row][i].ToString());
                            }
                            break; //We're done here!
                        }

                    }
                    else
                    {
                        Console.WriteLine("Name " + m_NameTextBox.Text + " was not found in the schedule file");
                        break;
                    }

                }
            }
            return toReturn;
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

                writer.WriteLine(UtilityData.ConfigProperties.Directory + "=");
                writer.WriteLine(UtilityData.ConfigProperties.NumberOfEmployees + "=10");
                writer.WriteLine(UtilityData.ConfigProperties.Name + "=");
                writer.WriteLine(UtilityData.ConfigProperties.Salary + "=");

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

                readString = ReadFromConfig(UtilityData.ConfigProperties.Directory);
                if (readString != "" && readString != null)
                    m_DirectoryTextBox.Text = readString;
                else
                    Console.WriteLine("Could not read SchedulePath from config. Is it empty?");

                readString = ReadFromConfig(UtilityData.ConfigProperties.NumberOfEmployees);
                if (readString != "" && readString != null)
                    m_NumberOfEmployees = Convert.ToUInt32(readString);
                else
                    Console.WriteLine("Could not read NumberOfEmployees from the config. Is it empty?");

                readString = ReadFromConfig(UtilityData.ConfigProperties.Name);
                if (readString != "" && readString != null)
                    m_NameTextBox.Text = readString;
                else
                    Console.WriteLine("Could not read Name from config. Is it empty?");

                readString = ReadFromConfig(UtilityData.ConfigProperties.Salary);
                if (readString != "" && readString != null)
                    m_SalaryTextBox.Text = readString;
                else
                    Console.WriteLine("Could not read Salary from config. Is it empty?");

                reader.Dispose();
            }
        }

        private void m_NameTextBox_TextChanged(object sender, EventArgs e)
        {
            m_NameTextBox.Text = SanteziseInput(m_NameTextBox.Text, true, true, false);
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
                    if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ')
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
            m_SalaryTextBox.Text = SanteziseInput(m_SalaryTextBox.Text, false, true, true);
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
                    writer.WriteLine(UtilityData.ConfigProperties.Directory + "=" + m_DirectoryTextBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.Directory + "=");

                //NumberOfEmplyees
                if (m_NumberOfEmployees != 0) //No need to check for null on uint
                    writer.WriteLine(UtilityData.ConfigProperties.NumberOfEmployees + "=" + Convert.ToString(m_NumberOfEmployees));
                else
                    writer.WriteLine(UtilityData.ConfigProperties.NumberOfEmployees + "=");

                //Name
                if (m_NameTextBox.Text != "" && m_NameTextBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.Name + "=" + m_NameTextBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.Name + "=");

                //Salary
                if (m_SalaryTextBox.Text != "" && m_SalaryTextBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.Salary + "=" + m_SalaryTextBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.Salary + "=");

                writer.Dispose();
            }
        }
    }
}