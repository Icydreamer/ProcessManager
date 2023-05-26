using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProcessMonitor
{

    /// <summary>
    /// 将监控结果记入CSV文件
    /// </summary>
    static class CsvWriter
    {
        public static void WriteRecordsToCsv(List<Record> records, string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("ProcessName,StartDateTime,Duration");

                foreach (var record in records)
                {
                    writer.WriteLine($"{record.ProcessName},{record.StartDateTime},{record.Duration}");
                }
            }
        }
    }
}