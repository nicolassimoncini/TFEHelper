using System;
using System.Collections.Generic;
using System.Text;

namespace TFEHelper.Backend.Tools.DateAndTime
{
    /// <summary>
    /// Extensions for <see cref="DateTime"/> type.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns an EOB version of the input date (time at 23:59:59:999).
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime Eob(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// Returns an BOB version of the input date (time at 00:00:00).
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime Bob(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
    }
}
