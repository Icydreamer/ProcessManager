using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Media3D;

namespace ProcessMonitor
{
    static class CsvWriter
    {
        public static void WriteRecordsToCsv(List<Record> records, string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("ProcessName,Duration");

                foreach (var record in records)
                {
                    writer.WriteLine($"{record.ProcessName},{record.FormattedDuration}");
                }
            }
        }
    }
}