using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Set : UserControl
    {
        public Set()
        {
            InitializeComponent();
        }

        //主题切换
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = new ResourceDictionary();
            if (Application.Current.Resources.MergedDictionaries[0].Source.ToString() == "pack://application:,,,/ProcessManager;component/Resources/color.xaml")
            {
                resource.Source = new Uri("pack://application:,,,/ProcessManager;component/Resources/darkcolor.xaml");
            }
            else
            {
                resource.Source = new Uri("pack://application:,,,/ProcessManager;component/Resources/color.xaml");
            }
            Application.Current.Resources.MergedDictionaries[0] = resource;

        }
        //导出csv文件
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

        }
        ////清除缓存
        //private void dataremove(object sender, RoutedEventArgs e)
        //{

        //}
        ////确认
        //private void sure(object sender, RoutedEventArgs e)
        //{

        //}
        ////取消
        //private void cancel(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
