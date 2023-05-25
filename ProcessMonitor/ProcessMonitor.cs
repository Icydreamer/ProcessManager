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
    class ProcessMonitor
    {
        private string activeProcessName;
        private Stopwatch stopwatch;
        private List<Record> records;

        public ProcessMonitor()
        {
            activeProcessName = GetActiveProcessName(); // ��ʼ��Ϊ��ǰ����������
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

                    Console.WriteLine("Ӧ��ʹ��ʱ����¼��");
                    foreach (var record in records)
                    {
                        var formattedDuration = DurationFormatter.FormatDuration(record.Duration);
                        Console.WriteLine($"Ӧ��: {record.ProcessName}, ʹ��ʱ��: {formattedDuration}");
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
