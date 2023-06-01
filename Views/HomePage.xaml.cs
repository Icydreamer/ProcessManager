using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MvvmTutorials.ToolkitMessages.Views;

namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UserControl
    {

        public HomePage()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string githubUrl = "https://github.com/Icydreamer/ProcessManager";

            // 创建一个启动信息对象
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c start {githubUrl}",  // 使用命令提示符打开默认浏览器并导航到链接
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // 启动进程
            Process.Start(startInfo);
        }
    }
}
