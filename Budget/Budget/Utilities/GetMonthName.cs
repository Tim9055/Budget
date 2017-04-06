using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Budget.Utilities
{
    public static class Get
    {
        public static string MonthName(int month)
        {
            return DateTimeFormatInfo.InvariantInfo.MonthNames[month - 1];
        }
    }
    
}