﻿using Microsoft.Win32;
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
using DataBase.Models;
using DataBase.Services;
using System.Drawing;
using System.Windows;
using System.Drawing.Imaging;

namespace ProcessMonitor
{
    public class ProcessMonitor
    {
        private string activeProcessName;
        private Stopwatch stopwatch;
        private List<Record> records;
        private bool isMonitoring;
        private readonly MyDbContext dbContext;
        private readonly DataBase.Services.DataBase dataBase;
        private readonly AppData appData;
        private readonly Data data;
        public ProcessMonitor()
        {
            activeProcessName = GetActiveProcessName(); // 初始化为当前活动的软件名称
            stopwatch = new Stopwatch();
            records = new List<Record>();
            isMonitoring = true;
            dbContext = new MyDbContext();
            dataBase = new DataBase.Services.DataBase(dbContext);
            appData = new AppData(dataBase);
            data = new Data(appData, dataBase);
            appData.Load();
        }

        public void StartMonitoring()
        {
            // 将 OnSessionSwitch 方法注册为 SessionSwitch 事件的处理程序
            SystemEvents.SessionSwitch += OnSessionSwitch;
            while (true)
            {
                if (isMonitoring)
                {
                    {
                        var currentProcessName = GetActiveProcessName();
                        // 排除无焦点的情况
                        if (currentProcessName != activeProcessName && currentProcessName != "Idle")
                        {
                            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                            int elapsedRoundedSeconds = elapsedSeconds < 1 ? 0 : (int)Math.Ceiling(elapsedSeconds);
                            Console.WriteLine($"应用: {activeProcessName} | 开始时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss} | 使用时长: {elapsedRoundedSeconds} 秒");
                            WriteLog($"应用: {activeProcessName} | 开始时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss} | 使用时长: {elapsedRoundedSeconds} 秒");

                            // 提取icon
                            SaveProcessIcon(activeProcessName);
                            string iconDirectory = "ico";
                            string iconFileName = $"{activeProcessName}.png";
                            string iconFilePath = Path.Combine(iconDirectory, iconFileName);
                            // 添加到AppModel表
                            appData.AddApp(new AppModel()
                            {
                                Name = activeProcessName,
                                Description = "test",
                                File = "test",
                                CategoryID = 0,
                                IconFile = iconFilePath,
                            });

                            // 更新description、file

                            data.SaveAppTime(activeProcessName, elapsedRoundedSeconds, DateTime.Now);

                            stopwatch.Restart();
                            activeProcessName = currentProcessName;
                            records.Add(new Record(activeProcessName, DateTime.Now, elapsedRoundedSeconds));

                            CsvWriter.WriteRecordsToCsv(records, "Record.csv");

                            stopwatch.Restart();
                            activeProcessName = currentProcessName;
                        }

                        Thread.Sleep(100);
                    }
                }
            }

        }

        /// <summary>
        /// 锁屏时暂停记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                isMonitoring = false;
                Console.WriteLine("因屏幕锁定，记录已暂停!");
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                isMonitoring = true;
                Console.WriteLine("因屏幕解锁，记录已恢复~");
            }
        }

        /// <summary>
        /// 获得当前处于Active的进程名称
        /// </summary>
        /// <returns></returns>
        private string GetActiveProcessName()
        {
            var activeProcess = GetActiveProcess();
            if (activeProcess != null)
            {
                return activeProcess.ProcessName;
            }
            return null;
        }

        /// <summary>
        /// 获得当前处于Active的进程ID
        /// </summary>
        /// <returns></returns>
        private Process GetActiveProcess()
        {
            var handle = GetForegroundWindow();
            GetWindowThreadProcessId(handle, out var processId);
            return Process.GetProcessById((int)processId);
        }

        // 获取当前前台窗口的句柄
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        // 获取该窗口所属的进程 ID
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public void WriteLog(string message)
        {
            string logDirectory = "Log";
            string logFileName = $"{DateTime.Now:yyyy-MM-dd}.log";
            string logFilePath = Path.Combine(logDirectory, logFileName);

            // 创建日志文件夹（如果不存在）
            Directory.CreateDirectory(logDirectory);

            // 写入日志
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($" {message}");
                writer.WriteLine("--------------------------------------------------------------------");
            }
        }

        // 获取进程的图标
        private void SaveProcessIcon(string processName)
        {
            string iconDirectory = "ico";
            string iconFileName = $"{processName}.png"; // 将文件扩展名改为 .png
            string iconFilePath = Path.Combine(iconDirectory, iconFileName);

            // 创建ico文件夹（如果不存在）
            Directory.CreateDirectory(iconDirectory);

            // 检查是否已存在png图标文件，如果存在则不执行任何操作
            if (File.Exists(iconFilePath))
            {
                return;
            }

            // 获取进程的图标
            using (Process process = Process.GetProcessesByName(processName).FirstOrDefault())
            {
                if (process != null)
                {
                    Icon processIcon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                    if (processIcon != null)
                    {
                        // 保存图标为png文件
                        using (FileStream stream = new FileStream(iconFilePath, FileMode.Create))
                        {
                            Bitmap bitmap = processIcon.ToBitmap();
                            bitmap.Save(stream, ImageFormat.Png);
                        }
                    }
                    else
                    {
                        // 处理图标为空的情况
                    }
                }
            }
        }
    }
}
