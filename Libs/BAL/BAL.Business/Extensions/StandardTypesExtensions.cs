using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Extensions
{
    public static class StandardTypesExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return FirstDayOfMonth(date).AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime LastDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }
    }
}
