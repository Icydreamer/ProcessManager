using System;
using System.Collections.Generic;
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
        //清除缓存
        private void dataremove(object sender, RoutedEventArgs e)
        {

        }
        //导出csv文件
        private void csv(object sender, RoutedEventArgs e)
        {

        }
        //确认
        private void sure(object sender, RoutedEventArgs e)
        {

        }
        //取消
        private void cancel(object sender, RoutedEventArgs e)
        {

        }
    }
}
