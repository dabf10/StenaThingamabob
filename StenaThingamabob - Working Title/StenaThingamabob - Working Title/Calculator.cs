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

        public UtilityData.WorkingHours CalculateMultipleWeeks(List<UtilityData.Week> weeks)
        {
            UtilityData.WorkingHours toReturn = new UtilityData.WorkingHours();

            UtilityData.WorkingHours calculatedWeek = null;
            foreach (UtilityData.Week week in weeks)
            {
                calculatedWeek = CalculateSingleWeekTotalTime(week);

                toReturn.Base += calculatedWeek.Base;
                toReturn.Part600 += calculatedWeek.Part600;
                toReturn.Part400 += calculatedWeek.Part400;
                toReturn.Part300 += calculatedWeek.Part300;
            }
            return toReturn;
        }
        /// <summary>
        /// Calculates Hours and OB for the given week
        /// </summary>
        /// <param name="toCalculate">The week to perform the calculations on</param>
        /// <returns>A copy of the calculated workingHours object</returns>
        private UtilityData.WorkingHours CalculateSingleWeekTotalTime(UtilityData.Week toCalculate)
        {
            if (!toCalculate.HoursCalculated()) //If the time has not already been calculated we do that now.
                    CalculateWorkingHoursWeek(toCalculate);

            UtilityData.WorkingHours toReturn = new UtilityData.WorkingHours();

            foreach (UtilityData.Day day in toCalculate.days)
            {
                toReturn.Base += day.workingPeriodOne.Hours.Base;
                toReturn.Base += day.workingPeriodTwo.Hours.Base;

                toReturn.Part600 += day.workingPeriodOne.Hours.Part600;
                toReturn.Part600 += day.workingPeriodTwo.Hours.Part600;

                toReturn.Part400 += day.workingPeriodOne.Hours.Part400;
                toReturn.Part400 += day.workingPeriodTwo.Hours.Part400;

                toReturn.Part300 += day.workingPeriodOne.Hours.Part300;
                toReturn.Part300 += day.workingPeriodTwo.Hours.Part300;
            }
            return toReturn;
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
            toCalculate.Hours.Base = Math.Abs(TimeStringToDouble(toCalculate.OutTime) - TimeStringToDouble(toCalculate.InTime));
            toCalculate.Hours.Part600 = toCalculate.IntersectionTime(UtilityData.WorkingPeriod.period600);
            toCalculate.Hours.Part400 = toCalculate.IntersectionTime(UtilityData.WorkingPeriod.period400);
            toCalculate.Hours.Part300 = toCalculate.IntersectionTime(UtilityData.WorkingPeriod.period300);

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