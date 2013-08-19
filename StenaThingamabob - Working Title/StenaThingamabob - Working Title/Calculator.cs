using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StenaThingamabob___Working_Title
{
    class Calculator
    {
        public Calculator()
        {

        }

        public double CalculateMultipleWeeksTotalTime(List<UtilityData.Week> weeks)
        {
            double toReturn = 0;

            foreach (UtilityData.Week week in weeks)
            {
                toReturn += CalculateSingleWeekTotalTime(week);
            }
            return toReturn;
        }

        private double CalculateSingleWeekTotalTime(UtilityData.Week toCalculate)
        {
            if (!toCalculate.HoursCalculated()) //If the time has not already been calculated we do that now.
                    CalculateWorkingHoursWeek(toCalculate);

            double weekTotalTime = 0d;

            foreach (UtilityData.Day day in toCalculate.days)
            {
                weekTotalTime += day.workingPeriodOne.Hours.Base;
                weekTotalTime += day.workingPeriodTwo.Hours.Base;
            }
            return weekTotalTime;
        }

        private void CalculateWorkingHoursWeek(UtilityData.Week ToCalculate)
        {
            if (ToCalculate.HoursCalculated())//Early Escape - This week has already been calculated!
                return;

            UtilityData.WorkingHours hoursOfWeek = new UtilityData.WorkingHours();

            foreach (UtilityData.Day day in ToCalculate.days)
            {
                hoursOfWeek += CalculateHoursOfPeriod(day.workingPeriodOne, day.weekend);
                hoursOfWeek += CalculateHoursOfPeriod(day.workingPeriodTwo, day.weekend);
            }
            hoursOfWeek.isCalculated = true;
            ToCalculate.Hours = hoursOfWeek;
        }
        /// <summary>
        /// Calculates the time components of a given workingperiod
        /// </summary>
        /// <param name="toCalculate">The working hours object to perform calculations on</param>
        /// <param name="weekend">Flag indicating if the inputed working hours object is a weekend</param>
        /// <returns>The hours object of the inputed working hours object</returns>
        private UtilityData.WorkingHours CalculateHoursOfPeriod(UtilityData.WorkingPeriod toCalculate, bool weekend)
        {
            toCalculate.Hours.Base = Math.Abs( TimeStringToDouble(toCalculate.OutTime) - TimeStringToDouble(toCalculate.InTime));
            //Calculate OB
            

            toCalculate.Hours.isCalculated = true;
            return toCalculate.Hours;
        }

        /// <summary>
        /// Converts a ´schedule file from its default string type to double
        /// </summary>
        /// <param name="toConvert">The time tring to be converted</param>
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
}