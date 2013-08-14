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
                weekTotalTime += day.workingPeriodOne.hours.Base;
                weekTotalTime += day.workingPeriodTwo.hours.Base;
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

        private UtilityData.WorkingHours CalculateHoursOfPeriod(UtilityData.WorkingPeriod toCalculate, bool weekend)
        {
            toCalculate.hours.Base = (TimeStringToDouble(toCalculate.outTime) - TimeStringToDouble(toCalculate.inTime));

            //TODO - Calculate OB

            toCalculate.hours.isCalculated = true;
            return toCalculate.hours;
        }

        private double TimeStringToDouble(string toConvert)
        {
            if (toConvert == "Ledig")
                return 0.0d;

            string[] parts = new string[2];
            parts = toConvert.Split(':');

            double toReturn = Convert.ToDouble(parts[0]);

            switch (Convert.ToUInt32(parts[1]))
            {
                case 0:
                        return toReturn;
                case 15:
                        return toReturn + 0.25d;
                case 30:
                        return toReturn + 0.5d;
                case 45:
                        return toReturn + 0.75d;
                default:
                        return -1.0d;
            }
        }
    }
}