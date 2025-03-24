using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgramming.Architecture.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string value)
        {
            return DateTime.Parse(value);
        }
    }
}
