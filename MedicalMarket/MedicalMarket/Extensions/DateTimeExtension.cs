using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMarket.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ToEgyptDateTime(this DateTime value)
        {
            if (value == null)
                return value;

            //Egypt Standard Time
            var toZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            return TimeZoneInfo.ConvertTime(value, toZone);
        }
    }
}
