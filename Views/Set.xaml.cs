using Microsoft.Win32;
using ProcessMonitor;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Set : UserControl
    {
        Config config;
        public Set()
        {
            InitializeComponent();
            config = Config.LoadConfig();
            Desktop_Reminder.IsChecked = config.NotificationEnabled;
            Auto_Start.IsChecked = config.StartupEnabled;
            if (config.ThemeColor == "light")
            {
                Color_Switch.IsChecked = false;
            }
            else
            {
                Color_Switch.IsChecked = true;
            }
        }

        //主题切换
        private void color_switch(object sender, RoutedEventArgs e)
        {
            if (Color_Switch.IsChecked == false)
            {
                EnableLightTheme();
                config.ThemeColor = "light";
            }
            else
            {
                EnableDarkTheme();
                config.ThemeColor = "dark";
            }
        }
        
        private void EnableLightTheme()
        {
            ResourceDictionary resource = new ResourceDictionary();
            resource.Source = new Uri("pack://application:,,,/ProcessManager;component/Resources/color.xaml");
            Application.Current.Resources.MergedDictionaries[0] = resource;
        }

        private void EnableDarkTheme()
        {
            ResourceDictionary resource = new ResourceDictionary();
            resource.Source = new Uri("pack://application:,,,/ProcessManager;component/Resources/darkcolor.xaml");
            Application.Current.Resources.MergedDictionaries[0] = resource;
        }

        // 另存为 CSV 文件
        private void export_csv(object sender, RoutedEventArgs e)
        {
            // 获取当前目录
            string currentDirectory = Directory.GetCurrentDirectory();
            string csvFilePath = System.IO.Path.Combine(currentDirectory, "Record.csv");

            // 创建文件选择对话框
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV文件 (*.csv)|*.csv";
            saveFileDialog.Title = "保存CSV文件";

            // 显示对话框并获取用户选择的保存路径
            if (saveFileDialog.ShowDialog() == true)
            {
                string saveFilePath = saveFileDialog.FileName;

                try
                {
                    // 复制文件到用户选择的路径
                    File.Copy(csvFilePath, saveFilePath, true);
                    MessageBox.Show("文件保存成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存文件时出现错误: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // 查看日志文件夹
        private void check_log(object sender, RoutedEventArgs e)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string logFolderPath = System.IO.Path.Combine(currentDirectory, "Log");

            if (Directory.Exists(logFolderPath))
            {
                Process.Start("explorer.exe", logFolderPath);
            }
            else
            {
                MessageBox.Show("日志文件夹不存在！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void auto_start(object sender, RoutedEventArgs e)
        {
            if (Auto_Start.IsChecked == true)
            {
                // 启用开机自启
                config.EnableStartup();
                config.StartupEnabled = true;
            }
            else
            {
                // 禁用开机自启
                config.DisableStartup();
                config.StartupEnabled = false;
            }
        }

        private void desktop_reminder(object sender, RoutedEventArgs e)
        {
            if (Desktop_Reminder.IsChecked == true)
            {
                config.NotificationEnabled = true;
            }
            else
            {
                config.NotificationEnabled = false;
            }
        }
    }
}
