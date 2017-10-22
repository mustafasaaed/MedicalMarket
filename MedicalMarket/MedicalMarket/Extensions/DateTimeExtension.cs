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
            if (value != null)
            {
                return value.ToLocalTime();
            }
            return value;
        }
    }
}
