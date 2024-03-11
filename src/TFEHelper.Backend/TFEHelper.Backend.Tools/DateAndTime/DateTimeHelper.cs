using System;
using System.Globalization;

namespace TFEHelper.Backend.Tools.DateAndTime
{
    /// <summary>
    /// Set of routines for handling date and time logic.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Calculates the quantity of working days considering:<br></br>
        ///  - weekend days (saturdays and sundays).<br></br>
        ///  - hollydays not included in weekends.
        /// </summary>
        /// <param name="firstDay">First day of the interval.</param>
        /// <param name="lastDay">Last day of the intervale.</param>
        /// <param name="holidays">List of hollydays excluding weekends.</param>
        /// <returns>Quantity of working days between <paramref name="firstDay"/> and <paramref name="lastDay"/>.</returns>
        public static int BusinessDaysUntil(this DateTime firstDay, DateTime lastDay, params DateTime[] holidays)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException($"First day {firstDay} cannot be greater than last day {lastDay}");

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = firstDay.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)firstDay.DayOfWeek;
                int lastDayOfWeek = lastDay.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)lastDay.DayOfWeek;

                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of holidays during the time interval
            foreach (DateTime holiday in holidays)
            {
                DateTime bh = holiday.Date;
                if (firstDay <= bh && bh <= lastDay)
                    --businessDays;
            }

            return businessDays;
        }

        /// <summary>
        /// Returns the weekend number according to ISO 8601.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear(DateTime date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Returns the first date of week accoring to its number.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns></returns>
        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            var ci = CultureInfo.InvariantCulture;

            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        /// <summary>
        /// Returns the first working day of a specific week.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns></returns>
        public static DateTime FirstWorkingDateOfWeek(int year, int weekOfYear)
        {
            return DateTimeHelper.FirstDateOfWeek(year, weekOfYear).AddDays(1);
        }

        /// <summary>
        /// Returns the last working day of a specific week.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns></returns>
        public static DateTime LastWorkingDateOfWeek(int year, int weekOfYear)
        {
            return FirstWorkingDateOfWeek(year, weekOfYear).AddDays(4);
        }
    }
}