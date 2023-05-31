using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ProcessMonitor;
using DataBase.Services;
using DataBase.Models;
using System.Threading;

namespace MvvmTutorials.ToolkitMessages
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            GlobalData.myDbContext = new MyDbContext();
            GlobalData.dataBase = new DataBase.Services.DataBase(GlobalData.myDbContext);
            GlobalData.AppDataInstance = new AppData(GlobalData.dataBase);
            GlobalData.DataInstance = new Data(GlobalData.AppDataInstance, GlobalData.dataBase);
            GlobalData.ProcessMonitorInstance = new ProcessMonitor.ProcessMonitor();
            GlobalData.AppDataInstance.Load();
            GlobalData.StartLoop();
        }
    }
    public static class GlobalData
    {
        public static  MyDbContext myDbContext { get; set; }
        public static  DataBase.Services.DataBase dataBase { get; set; }
        public static Data DataInstance { get; set; }
        public static AppData AppDataInstance { get; set; }
        public static ProcessMonitor.ProcessMonitor  ProcessMonitorInstance { get; set; }
        public static void StartLoop()
        {
            Thread thread = new Thread(() =>
            {
                ProcessMonitorInstance.StartMonitoring();
            });

            thread.IsBackground = true; // 设置为后台线程，以免影响应用程序关闭

            thread.Start();
        }
    }
    
}
