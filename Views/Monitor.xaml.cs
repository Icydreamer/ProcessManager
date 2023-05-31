using MvvmTutorials.ToolkitMessages.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Printing;
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
    /// Page5.xaml 的交互逻辑
    /// </summary>
    public partial class Monitor : UserControl
    {
        public Monitor()
        {
            InitializeComponent();
            //应用allapps里的app类，可考虑迁移
            for (int i = 0; i < 20; i++)
            {
                DataList.Add(new AllApps.app()
                {
                    Index = i,
                    ImgPath = "..\\Resources\\2.png",
                    Name = "index" + i,
                    IsSelected = true,
                    Time = 10 * i
                });
            }
            monitorapp.DataContext = this;

            //设置剩余时间条
            progressview Progressvalue = new progressview()
            {
                text = "今日剩余20分钟",
                value = 80
            };
    
            progress.DataContext = Progressvalue;



        }
        public ObservableCollection<AllApps.app> DataList { get; } = new ObservableCollection<AllApps.app>();

        //添加监视按钮
        private void monitor(object sender, RoutedEventArgs e)
        {
            //获取TextBlock.text,
        }
    }
    public class progressview
    {
        public string text { get; set; }
        public int value { get; set; }
    }
}