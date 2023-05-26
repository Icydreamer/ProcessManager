using System;

namespace ProcessMonitor
{
    class Record
    {
        public string ProcessName { get; set; }
        public TimeSpan Duration { get; set; }
        public string FormattedDuration { get; set; }

        public Record(string processName, TimeSpan duration)
        {
            ProcessName = processName;
            Duration = duration;
        }
    }
}