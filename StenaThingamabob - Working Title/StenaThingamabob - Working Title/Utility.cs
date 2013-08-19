namespace StenaThingamabob___Working_Title
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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

            public Day(WorkingPeriod PeriodOne, WorkingPeriod PeriodTwo, bool weekend)
                : this()
            {
                this.workingPeriodOne = PeriodOne;
                this.workingPeriodTwo = PeriodTwo;
                this.weekend = weekend;
            }

            public Day(WorkingPeriod periodOne, bool weekend)
                : this()
            {
                this.workingPeriodOne = periodOne;
                this.workingPeriodTwo = new WorkingPeriod("", "");
                this.weekend = weekend;
            }
        }

        public class WorkingPeriod
        {
            public string InTime { get; set; }
            public string OutTime { get; set; }
            public WorkingHours Hours { get; set; }

            public static WorkingPeriod period;

            private enum IntersectionType
            {
                None,               //No intersection
                Full,               //Full intersection
                SplitThis,          //"This" has to be split up since it crosses over two days
                SplitParameter,     //The input paramter has to be split up since it crosses over two days
                PartiallyBefore,    //"This" is partially intersecting and has parts earlier than the parameters start
                PartiallyAfter,     //"This" is partially intersecting and has parts later than the parameters end
                Contained           //"This" starts after and ends before the parameter
            };

            public WorkingPeriod()
            {
                this.InTime = string.Empty;
                this.OutTime = string.Empty;
                Hours = new WorkingHours();
            }

            public WorkingPeriod(string inTime, string outTime)
                : this()
            {
                this.InTime = inTime;
                this.OutTime = outTime;
                Hours = new WorkingHours();
            }

            public bool IsEmpty()
            {
                if (InTime == null && OutTime == null)
                {
                    return true;
                }
                return false;
            }

            public double IntersectionTime(WorkingPeriod toIntersect)
            {
                IntersectionType intersectionType = GetIntersectionType(toIntersect);

                uint toReturn = 0;
                switch (intersectionType)
                {
                    case IntersectionType.None:
                        {
                            return 0.0d;
                        }
                    case IntersectionType.Full:
                        {
                            return Math.Abs(TimeStringToDouble(toIntersect.OutTime) - TimeStringToDouble(toIntersect.InTime));
                        }
                    case IntersectionType.PartiallyAfter:
                        {
                            return Math.Abs(TimeStringToDouble(toIntersect.OutTime) - TimeStringToDouble(this.InTime));
                        }
                    case IntersectionType.PartiallyBefore:
                        {
                            return Math.Abs(TimeStringToDouble(this.OutTime) - TimeStringToDouble(toIntersect.InTime));
                        }
                    case IntersectionType.SplitParameter:
                        {
                            //TODO
                            break;
                        }
                    case IntersectionType.SplitThis:
                        {
                            //TODO
                            break;
                        }
                    case IntersectionType.Contained:
                        {
                            return Math.Abs(TimeStringToDouble(this.OutTime) - TimeStringToDouble(this.InTime));
                        }
                }
                return toReturn;
            }

            private IntersectionType GetIntersectionType(WorkingPeriod toIntersect)
            {
                if (this.InTime == this.OutTime || toIntersect.InTime == toIntersect.OutTime)
                    return IntersectionType.None;

                else if (TimeStringToDouble(this.InTime) < TimeStringToDouble(toIntersect.OutTime))
                {
                    if (TimeStringToDouble(this.OutTime) > TimeStringToDouble(toIntersect.InTime) && TimeStringToDouble(this.OutTime) < TimeStringToDouble(toIntersect.OutTime))
                        return IntersectionType.PartiallyBefore;

                    else if (TimeStringToDouble(this.OutTime) > TimeStringToDouble(toIntersect.OutTime))
                        return IntersectionType.Full;
                }

                else if (TimeStringToDouble(this.InTime) > TimeStringToDouble(toIntersect.OutTime))
                {
                    if (TimeStringToDouble(toIntersect.OutTime) > TimeStringToDouble(this.InTime) && TimeStringToDouble(toIntersect.OutTime) < TimeStringToDouble(this.OutTime))
                        return IntersectionType.PartiallyAfter;

                    else if (TimeStringToDouble(toIntersect.OutTime) > TimeStringToDouble(this.OutTime))
                        return IntersectionType.Contained;
                }
                else if (this.InTime == toIntersect.InTime && this.OutTime == toIntersect.OutTime)
                    return IntersectionType.Full;

                else if (TimeStringToDouble(this.InTime) > TimeStringToDouble(this.OutTime))
                    return IntersectionType.SplitThis;

                else if (TimeStringToDouble(toIntersect.InTime) > TimeStringToDouble(toIntersect.OutTime))
                    return IntersectionType.SplitParameter;

                return IntersectionType.None;
            }
            /// <summary>
            /// Converts a schedule file from its default string type to double
            /// </summary>
            /// <param name="toConvert">The time string to be converted</param>
            /// <returns>The inputed string as a double</returns>
            public static double TimeStringToDouble(string toConvert)
            {
                if (toConvert == "Ledig")
                    return 0.0d;
                else if (toConvert == string.Empty || toConvert == null)
                {
                    Console.WriteLine("Attempt to convert an empty time string to double was made");
                    return 0.0d;
                }
                else
                {
                    string[] parts = new string[2];
                    parts = toConvert.Split(':');

                    return Convert.ToDouble(parts[0]) + ((Convert.ToUInt32(parts[1]) / 60) * 100);
                }
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