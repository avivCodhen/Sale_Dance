using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Dance.Utility
{
    public static class DatetimeExtensions
    {
        public static string YearMonthDay(this DateTime s)
        {
            return $"{s:MMM d, yyyy}";
        }

        public static string DateWithTime(this DateTime s)
        {
            return $"{s:MMM d, yyyy, hh:mm tt}";
        }

    }
}
