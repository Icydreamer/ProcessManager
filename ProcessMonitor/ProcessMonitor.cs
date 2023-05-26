using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Media.Media3D;

namespace ProcessMonitor
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var config = Config.LoadConfig();
    //        string themeColor = config.ThemeColor;

    //        Console.WriteLine(themeColor);
    //        if (config.StartupEnabled)
    //        {
    //            // 执行开机自启逻辑
    //            Console.WriteLine("开机自启已启用。");
    //        }
    //        else
    //        {
    //            Console.WriteLine("开机自启已禁用。");
    //        }

    //        var processMonitorMonitor = new ProcessMonitor();
    //        processMonitorMonitor.StartMonitoring();

    //        Console.ReadLine();

    //    }
    //}
    class ProcessMonitor
    {
        private string activeProcessName;
        private Stopwatch stopwatch;
        private List<Record> records;

        public ProcessMonitor()
        {
            activeProcessName = GetActiveProcessName(); // │⌡╩╝╗»╬¬╡▒╟░╗ε╢»╡─╚φ╝■├√│╞
            stopwatch = new Stopwatch();
            records = new List<Record>();
        }

        public void StartMonitoring()
        {
            while (true)
            {
                var currentProcessNameName = GetActiveProcessName();

                if (currentProcessNameName != activeProcessName && currentProcessNameName != "Idle")
                {
                    var elapsed = stopwatch.Elapsed;

                    var existingRecord = records.FirstOrDefault(r => r.ProcessName == activeProcessName);

                    if (existingRecord != null)
                    {
                        existingRecord.Duration += elapsed;
                    }
                    else
                    {
                        records.Add(new Record(activeProcessName, elapsed));
                    }

                    records = records.OrderByDescending(r => r.Duration).ToList();

                    Console.WriteLine("╙ª╙├╩╣╙├╩▒│ñ╝╟┬╝ú║");
                    foreach (var record in records)
                    {
                        var formattedDuration = DurationFormatter.FormatDuration(record.Duration);
                        Console.WriteLine($"╙ª╙├: {record.ProcessName}, ╩╣╙├╩▒│ñ: {formattedDuration}");
                        record.FormattedDuration = formattedDuration;
                    }

                    CsvWriter.WriteRecordsToCsv(records, "Record.csv");

                    stopwatch.Restart();
                    activeProcessName = currentProcessNameName;
                }

                Thread.Sleep(10);
            }
        }

        private string GetActiveProcessName()
        {
            var activeProcess = GetActiveProcess();
            if (activeProcess != null)
            {
                return activeProcess.ProcessName;
            }
            return null;
        }

        private Process GetActiveProcess()
        {
            var handle = GetForegroundWindow();
            GetWindowThreadProcessId(handle, out var processId);
            return Process.GetProcessById((int)processId);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
    }
}
