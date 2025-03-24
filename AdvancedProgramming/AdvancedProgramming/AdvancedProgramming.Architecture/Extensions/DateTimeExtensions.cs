using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Architecture.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateTimeString(this DateTime? value)
        {
            return (value != null) 
                ? ((DateTime)value).ToShortDateString()
                : DateTime.UtcNow.ToShortDateString();
        }

        public static string ToDateTimeString(this DateTime value)
        {
            return value.ToShortDateString();
        }
    }
}
