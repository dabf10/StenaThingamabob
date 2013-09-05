using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace StenaThingamabob___Working_Title
{
    class ConfigManager
    {
        TextBox nameBox = null, directoryBox = null, salaryBox = null, yearBox = null;

        public ConfigManager(TextBox nameBox, TextBox directoryBox, TextBox salaryBox, TextBox yearBox)
        {
            this.nameBox = nameBox;
            this.directoryBox = directoryBox;
            this.salaryBox = salaryBox;
            this.yearBox = yearBox;
        }

        public void Initialize(ref decimal selectedWeekFrom, ref decimal selectedWeekTo)
        {
            if(!ConfigFileExists())
                CreateConfigFile();
            LoadConfig(ref selectedWeekFrom, ref selectedWeekTo);
        }

        /// <summary>
        /// Saves all config properties to the config file
        /// </summary>
        public void SaveConfig(ref decimal selectedWeekFrom, ref decimal selectedWeekTo)
        {
            if (File.Exists(UtilityData.FilePaths.ConfigPath))
            {
                StreamWriter writer = new StreamWriter(UtilityData.FilePaths.ConfigPath);

                //Directory
                if (directoryBox.Text != "" && directoryBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.directory + '=' + directoryBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.directory + '=');

                //Name
                if (nameBox.Text != "" && nameBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.name + '=' + nameBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.name + '=');

                //Salary
                if (salaryBox.Text != "" && salaryBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.salary + '=' + salaryBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.salary + '=');

                //Year
                if (yearBox.Text != "" && yearBox.Text != null)
                    writer.WriteLine(UtilityData.ConfigProperties.year + '=' + yearBox.Text);
                else
                    writer.WriteLine(UtilityData.ConfigProperties.year + '=');

                //WeekFrom
                    writer.WriteLine(UtilityData.ConfigProperties.weekSelectorFrom + '=' + selectedWeekFrom.ToString());

                //WeekTo
                    writer.WriteLine(UtilityData.ConfigProperties.weekSelectorTo + '=' + selectedWeekTo.ToString());

                writer.Dispose();
            }
        }

        /// <summary>
        /// Loads the config file
        /// </summary>
        public void LoadConfig(ref decimal selectedWeekFrom, ref decimal selectedWeekTo)
        {
            if (File.Exists(UtilityData.FilePaths.ConfigPath))
            {
                StreamReader reader = new StreamReader(UtilityData.FilePaths.ConfigPath);
                string readString = "";

                readString = ReadFromConfig(UtilityData.ConfigProperties.directory);
                if (readString != "" && readString != null)
                    directoryBox.Text = readString;
                else
                    Console.WriteLine("Could not read SchedulePath from config. Is it empty?");

                readString = ReadFromConfig(UtilityData.ConfigProperties.name);
                if (readString != "" && readString != null)
                    nameBox.Text = readString;
                else
                    Console.WriteLine("Could not read Name from config. Is it empty?");

                readString = ReadFromConfig(UtilityData.ConfigProperties.salary);
                if (readString != "" && readString != null)
                    salaryBox.Text = readString;
                else
                    Console.WriteLine("Could not read Salary from config. Is it empty?");

                readString = ReadFromConfig(UtilityData.ConfigProperties.year);
                if (readString != "" && readString != null)
                    yearBox.Text = readString;

                readString = ReadFromConfig(UtilityData.ConfigProperties.weekSelectorFrom);
                if (readString != "" && readString != null)
                    selectedWeekFrom = Convert.ToDecimal(readString);

                readString = ReadFromConfig(UtilityData.ConfigProperties.weekSelectorTo);
                if (readString != "" && readString != null)
                    selectedWeekTo = Convert.ToDecimal(readString);

                reader.Dispose();
            }
        }

        /// <summary>
        /// Reads the value of the inputed property in the config file
        /// </summary>
        /// <param name="propertyName"> The name of the property from wich to read the value</param>
        /// <returns>The value of the inputed property as a string. Returns empty string if the property was not found or was not set.</returns>
        private string ReadFromConfig(string propertyName)
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

        /// <summary>
        /// Creates the config file and assigns its default values
        /// </summary>
        private void CreateConfigFile()
        {
            if (!File.Exists(UtilityData.FilePaths.ConfigPath))
            {
                StreamWriter writer = new StreamWriter(UtilityData.FilePaths.ConfigPath);

                writer.WriteLine(UtilityData.ConfigProperties.directory + '=');
                writer.WriteLine(UtilityData.ConfigProperties.name + '=');
                writer.WriteLine(UtilityData.ConfigProperties.salary + '=');
                writer.WriteLine(UtilityData.ConfigProperties.year + '=');
                writer.WriteLine(UtilityData.ConfigProperties.weekSelectorFrom + '=');
                writer.WriteLine(UtilityData.ConfigProperties.weekSelectorTo + '=');

                writer.Dispose();
            }
        }

        /// <summary>
        /// Checks if the config file is already created
        /// </summary>
        /// <returns>True if the file exists</returns>
        private bool ConfigFileExists()
        {
            if (File.Exists(UtilityData.FilePaths.ConfigPath))
                return true;
            return false;
        }
    }
}
