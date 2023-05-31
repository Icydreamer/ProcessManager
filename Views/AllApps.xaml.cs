using MvvmTutorials.ToolkitMessages.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using DataBase.Models;

namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class AllApps : UserControl
    {
        public AllApps()
        {
            InitializeComponent();

            //右下角文字数据更改
            textcombine Allappstext = new textcombine();
            Allappstext.text1 = "本周共使用20个应用程序";
            Allappstext.text2 = "一共100分钟";
            Allappstext.text3 = "平均一天使用14分钟";
            Allapptexts.DataContext = Allappstext;

            //左侧应用修改
            for (int i = 0; i < 20; i++)
            {
                DataList.Add(new app()
                {
                    Index = i,
                    ImgPath = "..\\Resources\\2.png",
                    Name = "index" + i,
                    IsSelected = true,
                    Time = 10 * i
                });

            }*/
            foreach(var i in appList)
            {
                DataList.Add(new Foo()
                {
                    Index = i.ID,
                    ImgPath = i.IconFile,
                    Name = i.Name,
                    IsSelected = true,
                    Type = "typei",
                    Remark = "doubi",
                    TotalTime=i.TotalTime
                });
            }
            Appname.DataContext = this;
        }

        //文字类
        public class textcombine
        {
            public string text1 { get; set; }
            public string text2 { get; set; }
            public string text3 { get; set; }
        }

        //应用类
        public ObservableCollection<app> DataList { get; } = new ObservableCollection<app>();
        //可考虑迁移
        public class app
        {
            public int Index { get; set; }
            public string ImgPath { get; set; }
            public string Name { get; set; }
            public bool IsSelected { set; get; }
            public int Time { get; set; }

            
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

        //确认按钮
        private void sure(object sender, RoutedEventArgs e)
        { 
            //更改DataList的值
        }

        //切换视图按钮
        private void style (object sender, RoutedEventArgs e)
        {
            //更改绘制视图的方法
        }

        //textbox相关内容指令！！！

    }


}
