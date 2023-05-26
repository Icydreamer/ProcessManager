using System;

namespace ProcessMonitor
{
    static class DurationFormatter
    {
        public static string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalHours >= 1)
            {
                var hours = (int)duration.TotalHours;
                var minutes = duration.Minutes;
                return $"{hours}小时{minutes}分";
            }
            else
            {
                var minutes = (int)duration.TotalMinutes;
                var seconds = duration.Seconds;
                return $"{minutes}分钟{seconds}秒";
            }
        }
    }
}