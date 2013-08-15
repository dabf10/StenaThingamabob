using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace StenaThingamabob___Working_Title
{
    class ScheduleManager
    {
        Dictionary<string, DataTable> m_ScheduleData = new Dictionary<string, DataTable>(); //Key = sheetname Value = Loaded .xls sheet as DataTable
        Dictionary<uint, string> m_SheetNameYear = new Dictionary<uint, string>(); //Key = the current year. Value = The year specific part of the key to the scheduleData
        List<UtilityData.Week> m_LoadedWeeks = new List<UtilityData.Week>(); //Contains filled week containers

        string lastLoadedName = null;
        uint lastLoadedYear = 0;

        public enum PeriodType
        {
            Unknown, //This is set if the type cannot be guessed

            Free, //NULL | NULL | NULL
            Normal, //In Time | NULL | Out Time
            WeekDayDouble, // In Time - Out Time | NULL | In Time - Out Time
            WeekDayOnlyFirstCell, // In Time - Out Time | NULL | NULL
            WeekDayOnlySecondCell, // NULL | NULL | In Time - Out Time

            WeekendEarly, // In Time | NULL | Out Time | NULL | NULL | NULL
            WeekendLate, // NULL | NULL | NULL | In Time | NULL | Out Time
            WeekendDouble, // In Time | NULL | Out Time | In Time | NULL | Out Time

            WierdStringContainingYearFirstCell, //Cannot predict when these will show up. Schedule file seems normal at the cells of occurance.
            WierdStringContainingYearSecondCell
        }

        public ScheduleManager()
        {
            FillSheetNameYearDictionary();
        }

        /// <summary>
        /// Tries to load the schedule from the inputed path using an OleDbConnection. If successfull the path is saved to "LastPath.txt".
        /// </summary>
        /// <param name="filePath">The path to the schedule file(.xls/.xlsx)</param>

        public bool LoadSchedule(string filePath, uint year)
        {
            //Validate the parameters
            if (filePath == null || filePath == string.Empty || year == 0)
            {
                Console.WriteLine("Invalid parameter supplied to LoadSchedule");
                return false;
            }

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
                return false;
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
                string sheetYearPrefix = null;
                string sheetNumber = null;
                string sheetName = null;
                sheetYearPrefix = m_SheetNameYear[year]; //Already protected by a try statement
                for (int i = 0; i < 32; ++i) //Hard code - WeekNumber
                {
                    sheetData = new DataTable();
                    sheetNumber = (i + 1).ToString();
                    sheetName = sheetYearPrefix + sheetNumber;
                    command.CommandText = @"SELECT * FROM [" + sheetName + "$]";
                    adapter.Fill(sheetData);
                    m_ScheduleData.Add(sheetName, sheetData);
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
            if (m_ScheduleData.Count > 0)
                return true;
            else
                return false;
        }

        public void UnloadWeeks()
        {
            if(m_LoadedWeeks.Count > 0)
                m_LoadedWeeks.RemoveRange(0, m_LoadedWeeks.Count() - 1);
        }

        public bool LoadWeeks(string name, uint year)
        {
            if (WeeksLoaded(name, year))
                UnloadWeeks();

            List<string> scheduleRow = new List<string>();
            for (uint i = 0; i < NumberOfWeeks(); ++i)
            {
                scheduleRow = FindScheduleRowOnName(name,year, i);
                if (scheduleRow.Count <= 0)
                    return false;
                m_LoadedWeeks.Add(CreateWeek(scheduleRow, i));
            }

            if (m_LoadedWeeks.Count() > 0)
            {
                lastLoadedName = name;
                lastLoadedYear = year;
                return true;
            }
            else
                return false;

        }

        /// <summary>
        /// Checks if there are any entries in the schedule data dictionary
        /// </summary>
        /// <returns>True if schedule is loaded.</returns>
        public bool ScheduleLoaded()
        {
            if (m_ScheduleData.Count() > 0)
                return true;
            return false;
        }

        public bool WeeksLoaded(string name, uint year)
        {
            if (lastLoadedName == name && lastLoadedYear == year)
                return true;
            return false;
        }

        /// <summary>
        /// Returns the number of weeks found in the schedule file the current year
        /// </summary>
        /// <returns>Number of weeks found in the scedule file</returns>
        public int NumberOfWeeks()
        {
            return m_ScheduleData.Count();
        }

        public List<UtilityData.Week> GetWeeks(uint from, uint to)
        {
            return m_LoadedWeeks.GetRange((int)(from - 1), (int)(to - (from - 1)));
        }

        public uint NumberOfWeeks(string name, uint year)
        {
            if (ScheduleLoaded())
            {
                if (WeeksLoaded(name, year))
                    return (uint)m_LoadedWeeks.Count();
                else
                {
                    if (LoadWeeks(name, year))
                        return (uint)m_LoadedWeeks.Count();
                    else
                        return 0; //There was an error loading the weeks from the saved data sheets
                }
            }
            else
                return 0;
        }

        public bool YearExists(uint year)
        {
            if (m_SheetNameYear.ContainsKey(year))
                return true;
            return false;
        }

        private PeriodType GuessPeriodType(List<string> cells)
        {
            PeriodType toReturn = PeriodType.Unknown;

            if (cells.Count() == 3)
            {
                if (cells[0] == "")
                {
                    if (cells[2] == "")
                        toReturn = PeriodType.Free;

                    else if (cells[2].Contains('-'))
                    {
                        if (!cells[2].Contains(' '))
                            toReturn = PeriodType.WeekDayOnlySecondCell;
                    }
                }
                else
                {
                    if (cells[0].Contains('-'))
                    {
                        if (cells[0].Contains(' '))
                            toReturn = PeriodType.WierdStringContainingYearFirstCell;
                        else if (cells[2].Contains('-'))
                            toReturn = PeriodType.WeekDayDouble;
                        else if (cells[2] == "")
                            toReturn = PeriodType.WeekDayOnlyFirstCell;

                    }
                    else if (cells[2].Contains(' '))
                        toReturn = PeriodType.WierdStringContainingYearSecondCell;

                    else if (cells[2] != "")
                    {
                        toReturn = PeriodType.Normal;
                    }
                }
            }
            else if (cells.Count() == 6)
            {
                if (cells[0] == "" && cells[2] == "")
                {
                    if (cells[3] == "" && cells[5] == "")
                        toReturn = PeriodType.Free;
                    else
                        toReturn = PeriodType.WeekendLate;
                }
                else
                {
                    if (cells[3] == "" && cells[5] == "")
                        toReturn = PeriodType.WeekendEarly;
                    else
                        toReturn = PeriodType.WeekendDouble;
                }
            }
            if (toReturn == PeriodType.Unknown)
                Console.WriteLine("ScheduleManager - GuessPeriodType was unable to guess the type of the following cells: " + cells.ToString());
                return toReturn;   
        }

        /// <summary>
        /// Finds the cells belonging to a given name in the schedule
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <returns>List of strings containing the cell data</returns>
        private List<string> FindScheduleRowOnName(string name, uint year,uint weekNumber )
        {
            weekNumber += 1;

            int stenaLineLabelsFound = 0;
            List<string> toReturn = new List<string>();
            string currentSheetYearPrefix = m_SheetNameYear[year];
            string currentSheetWeek = null;
            currentSheetWeek = weekNumber.ToString();
            string currentSheet = currentSheetYearPrefix + currentSheetWeek;
            string cellData = string.Empty;
            for (int column = 0; column < 55; ++column)
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
                        else if (cellData == name)
                        {
                            for (int i = 1; i <= 27; ++i) //Hard code 
                            {
                                toReturn.Add(m_ScheduleData[currentSheet].Rows[row][i].ToString());
                            }
                            return toReturn; //We're done here!
                        }
                    }
                    else
                    {
                        Console.WriteLine("Name " + name + " was not found in the schedule file");
                        return toReturn;
                    }
                }
            }
            return toReturn;
        }

        private UtilityData.Week CreateWeek(List<string> scheduleRow, uint weekNumber)
        {
            UtilityData.Day[] days = new UtilityData.Day[7];
            int currentDay = 0;

            PeriodType periodType = PeriodType.Unknown;
            int cellsRead = 0;
            
            while (cellsRead < scheduleRow.Count())
            {
                UtilityData.WorkingPeriod periodOne = new UtilityData.WorkingPeriod();
                UtilityData.WorkingPeriod periodTwo = new UtilityData.WorkingPeriod();
                if (currentDay < 5)
                {
                    periodType = GuessPeriodType(scheduleRow.GetRange(cellsRead, 3));
                    cellsRead += 3;
                }
                else
                {
                    periodType = GuessPeriodType(scheduleRow.GetRange(cellsRead, 6));
                    cellsRead += 6;
                }

                switch (periodType)
                {
                    case (PeriodType.Free):
                        {
                            periodOne.inTime = "Ledig";
                            periodOne.outTime = "Ledig";
                            periodTwo.inTime = "Ledig";
                            periodTwo.outTime = "Ledig";
                            break;
                        }

                    case (PeriodType.Normal):
                        {
                            periodOne.inTime = scheduleRow[currentDay * 3];
                            periodOne.outTime = scheduleRow[currentDay * 3 + 2];
                            periodTwo.inTime = "Ledig";
                            periodTwo.outTime = "Ledig";
                            break;
                        }

                    case (PeriodType.WeekDayDouble):
                        {
                            string[] parts = scheduleRow[currentDay * 3].Split('-');
                            periodOne.inTime = parts[0];
                            periodOne.outTime = parts[1];

                            parts = scheduleRow[currentDay * 3 + 2].Split('-');
                            periodTwo.inTime = parts[0];
                            periodTwo.outTime = parts[1];
                            
                            break;
                        }

                    case (PeriodType.WeekDayOnlyFirstCell):
                        {
                            string[] parts = scheduleRow[currentDay * 3].Split('-');
                            periodOne.inTime = parts[0];
                            periodOne.outTime = parts[1];
                            periodTwo.inTime = "Ledig";
                            periodTwo.outTime = "Ledig";
                            break;
                        }
                    case (PeriodType.WeekDayOnlySecondCell):
                        {
                            string[] parts = scheduleRow[currentDay * 3 + 2].Split('-');
                            periodOne.inTime = parts[0];
                            periodOne.outTime = parts[1];
                            periodTwo.inTime = "Ledig";
                            periodTwo.outTime = "Ledig";
                            break;
                        }
                    case (PeriodType.WeekendDouble):
                        {
                            periodOne.inTime = scheduleRow[cellsRead - 6];
                            periodOne.outTime = scheduleRow[cellsRead - 4];
                            periodTwo.inTime = scheduleRow[cellsRead - 3];
                            periodTwo.outTime = scheduleRow[cellsRead - 1];
                            break;
                        }
                    case (PeriodType.WeekendEarly):
                        {
                            periodOne.inTime = scheduleRow[cellsRead - 6];
                            periodOne.outTime = scheduleRow[cellsRead - 4];
                            periodTwo.inTime = "Ledig";
                            periodTwo.outTime = "Ledig";
                            break;
                        }
                    case (PeriodType.WeekendLate):
                        {
                            periodOne.inTime = scheduleRow[cellsRead - 3];
                            periodOne.outTime = scheduleRow[cellsRead - 1];
                            periodTwo.inTime = "Ledig";
                            periodTwo.outTime = "Ledig";
                            break;
                        }
                    case (PeriodType.WierdStringContainingYearFirstCell):
                        {
                            string[] parts = scheduleRow[cellsRead - 3].Split(' ');
                            string[] partsparts = parts[1].Split(':');
                            periodOne.inTime = partsparts[0] + ':' + partsparts[1];
                            periodOne.outTime = scheduleRow[cellsRead];
                            periodTwo.inTime = "Ledig";
                            periodTwo.outTime = "Ledig";
                            break;
                        }
                    case(PeriodType.WierdStringContainingYearSecondCell):
                        {
                            string[] parts = scheduleRow[cellsRead - 1].Split(' ');
                            string[] partsparts = parts[1].Split(':');
                            periodOne.inTime = scheduleRow[cellsRead - 3];
                            periodOne.outTime = partsparts[0] + ':' + partsparts[1];
                            periodTwo.inTime = "Ledig";
                            periodTwo.outTime = "Ledig";
                            break;
                        }
                }

                if (currentDay < 5)
                    days[currentDay] = new UtilityData.Day(periodOne, periodTwo, false);
                else
                    days[currentDay] = new UtilityData.Day(periodOne, periodTwo, true);

                ++currentDay;
            }
            if (weekNumber == 6)
                Console.Write(' ');
            return new UtilityData.Week(weekNumber, days);
        }

        private void FillSheetNameYearDictionary()
        {
            m_SheetNameYear.Add(2013, "ve");
        }
    }
}