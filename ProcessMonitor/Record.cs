using System;

namespace ProcessMonitor
{

    /// <summary>
    /// 定义Record类
    /// </summary>
    class Record
    {
        public string ProcessName { get; set; }
        public int Duration { get; set; }

        // public string FormattedDuration { get; set; }
        public DateTime StartDateTime { get; set; }

        public Record(string processName, DateTime startDateTime, int duration)
        {
            ProcessName = processName;
            Duration = duration;
            StartDateTime = DateTime.Now;
        }
    }
}