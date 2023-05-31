using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HandyControl.Data;
using Microsoft.Win32;
using MvvmTutorials.ToolkitMessages.Views;
using MvvmTutorials.ToolkitMessages.Views.AllApps;

namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class SingleApp : UserControl
    {
        public SingleApp()
        {
            InitializeComponent();
            //应用allapps里的app类，可考虑迁移
            AllApps.app Singleapp = new AllApps.app() { Index = 0,
                    ImgPath = "..\\Resources\\2.png",
                    Name = "index0",
                    IsSelected = true,
                    Time = 100 };
            //绑定数据
            singleapp.DataContext = Singleapp;


            //左下角文字数据更改
            textcombine Allappstext = new textcombine();
            Allappstext.text1 = "距离上次使用已经过去60分钟";
            Allappstext.text2 = "本周共使用8个小时";
            Allappstext.text3 = "平均每天使用1个小时";
            Allapptexts.DataContext = Allappstext;


            //展开框的搜索栏更改
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
            Appname.DataContext = this;
        }
        public ObservableCollection<AllApps.app> DataList { get; } = new ObservableCollection<AllApps.app>();

        //手动添加exe文件按钮
        private void add(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择数据源文件";
            openFileDialog.Filter = "exe文件|*.exe";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "exe";
            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }
            //返回值
            string txtFile = openFileDialog.FileName;

        }

        //三个按钮
        private void Month(object sender, RoutedEventArgs e)
        {
            //添加默认起始日期
            box.Text = "2023.3.1";
            //其他数据更改

        }
        private void Week(object sender, RoutedEventArgs e)
        {
            //添加默认起始日期
            box.Text = "2023.2.1";
            //其他数据更改

        }

        private void Day(object sender, RoutedEventArgs e)
        {
            //添加默认起始日期
            box.Text = "2023.1.1";
            //其他数据更改

        }


        //切换视图按钮
        private void style(object sender, RoutedEventArgs e)
        {
            //更改绘制视图的方法
        }


        //实例化自定义command：searchcommand!!!
        //textbox相关内容指令！！
    }
}
