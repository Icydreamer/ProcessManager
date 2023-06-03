using Microsoft.Win32.TaskScheduler;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Security;
using System.Text.Json;

namespace ProcessMonitor
{
    /// <summary>
    /// 定义配置项
    /// </summary>
    class Config
    {
        private bool startupEnabled;
        public bool StartupEnabled
        {
            get { return startupEnabled; }
            set
            {
                startupEnabled = value;
                UpdateConfigFile();
            }
        }

        private string themeColor;
        public string ThemeColor
        {
            get { return themeColor; }
            set
            {
                themeColor = value;
                UpdateConfigFile();
            }
        }

        private bool notificationEnabled;
        public bool NotificationEnabled
        {
            get { return notificationEnabled; }
            set
            {
                notificationEnabled = value;
                UpdateConfigFile();
            }
        }

        private static string configFilePath = "config.json";

        public static Config LoadConfig()
        {
            if (File.Exists(configFilePath))
            {
                string json = File.ReadAllText(configFilePath);
                Config config = JsonSerializer.Deserialize<Config>(json);
                return config;
            }
            else
            {
                Config newConfig = new Config
                {
                    StartupEnabled = false,
                    ThemeColor = "light",
                    NotificationEnabled = false,
                };

                string json = JsonSerializer.Serialize(newConfig, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(configFilePath, json);

                return newConfig;
            }
        }

        private void UpdateConfigFile()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(configFilePath, json);
        }

        public void EnableStartup()
        {
            AutoStart.Create(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup), "ProcessMonitor", Process.GetCurrentProcess().MainModule.FileName, "白驹-高效进程管理工具", ".\\Resources\\1.ico");
        }

        public void DisableStartup()
        {
            AutoStart.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup), "ProcessMonitor");
        }
    }
}