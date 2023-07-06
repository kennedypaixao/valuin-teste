using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edesoft.ERP.Tools.Extensions
{
    public static class TimeSpanExtension
    {
        public static string ToHumanTimeString(this TimeSpan span, int significantDigits = 3)
        {
            var format = "G" + significantDigits;
            return span.TotalMilliseconds < 1000 ? span.TotalMilliseconds.ToString(format) + " milisegundos"
                : (span.TotalSeconds < 60 ? span.TotalSeconds.ToString(format) + " segundos"
                    : (span.TotalMinutes < 60 ? span.TotalMinutes.ToString(format) + " minutos"
                        : (span.TotalHours < 24 ? span.TotalHours.ToString(format) + " horas"
                                                : span.TotalDays.ToString(format) + " dias")));
        }

        public static bool Between(this TimeSpan now, TimeSpan start, TimeSpan end)
        {
            if (start < end)
                return start <= now && now <= end;

            return !(end <= now && now <= start);
        }
    }
}
