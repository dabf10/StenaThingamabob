namespace StenaThingamabob___Working_Title
{
    namespace UtilityData
    {
        public struct FilePaths
        {
            public static string ConfigPath = System.Windows.Forms.Application.StartupPath + "Config.txt";
        }

        public struct ConfigProperties
        {
            public const string directory = "SchedulePath";
            public const string name = "Name";
            public const string salary = "Salary";
            public const string year = "Year";
        }

        public class Week
        {
            public Day[] days { get; private set; }
            public uint WeekNumber { get; private set; }
            public WorkingHours Hours { get; set; }

            public Week()
            { }

            public Week(uint weekNumber, Day[] days)
            {
                this.WeekNumber = weekNumber;
                this.days = days;
                Hours = new WorkingHours();
            }

            public bool HoursCalculated()
            {
                if (Hours.isCalculated)
                    return true;
                return false;
            }
        }

        public class Day
        {
            public bool weekend { get; private set; }
            public WorkingPeriod workingPeriodOne { get; private set; }
            public WorkingPeriod workingPeriodTwo { get; private set; }

            public Day()
            { }

            public Day(WorkingPeriod PeriodOne, WorkingPeriod PeriodTwo, bool weekend) : this()
            {
                this.workingPeriodOne = PeriodOne;
                this.workingPeriodTwo = PeriodTwo;
                this.weekend = weekend;
            }

            public Day(WorkingPeriod periodOne, bool weekend) : this()
            {
                this.workingPeriodOne = periodOne;
                this.workingPeriodTwo = new WorkingPeriod("", "");
                this.weekend = weekend;
            }
        }

        public class WorkingPeriod
        {
            public string inTime { get; set; }
            public string outTime { get; set; }
            public WorkingHours hours { get; set; }

            public WorkingPeriod()
            {
                this.inTime = string.Empty;
                this.outTime = string.Empty;
                hours = new WorkingHours();
            }

            public WorkingPeriod(string inTime, string outTime) : this()
            {
                this.inTime = inTime;
                this.outTime = outTime;
                hours = new WorkingHours();
            }

            public bool IsEmpty()
            {
                if (inTime == null && outTime == null)
                {
                    return true;
                }
                return false;
            }
        }

        public class WorkingHours
        {
            public double Base { get; set; }
            public double Part600 { get; set; }
            public double Part400 { get; set; }
            public double Part300 { get; set; }
            public bool isCalculated { get; set; }

            public WorkingHours()
            { }

            public WorkingHours(double baseHours, double part600, double part400, double part300, bool isCalculated = true)
                : this()
            {
                this.Base = baseHours;
                this.Part600 = part600;
                this.Part400 = part400;
                this.Part300 = part300;

                this.isCalculated = isCalculated;
            }

            public static WorkingHours operator +(WorkingHours LHS, WorkingHours RHS)
            {
                return new WorkingHours(LHS.Base + RHS.Base, LHS.Part600 + RHS.Part600, LHS.Part400 + RHS.Part400, LHS.Part300 + RHS.Part300);
            }
        }
    }
}
