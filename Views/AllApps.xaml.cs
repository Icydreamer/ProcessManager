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
            var appList=GlobalData.AppDataInstance.GetAllApps();

            //左侧应用修改
            foreach(var i in appList)
            {
                DataList.Add(new app()
                {
                    Index = i.ID,
                    ImgPath = i.IconFile,
                    Name = i.Name,
                    IsSelected = false,
                    Time=i.TotalTime
                });
            }
            Appname.DataContext = this;

            //右侧时间折线图

            /*                var totayAllAppsTime = GlobalData.DataInstance.GetAppDayData(1, DateTime.Now);
                            List<AppTimer> appTimers = new List<AppTimer>();
                            foreach(var timer in totayAllAppsTime)
                            {
                                appTimers.Add(new AppTimer(timer.DataTime.ToString(), timer.Time));
                            }
                        test.appTimersAll = appTimers;*/
            AllTimer test = new AllTimer();
             AppTimers.DataContext = test;


        }
        public class Person
        {
            public string Name { get; set; }

            public double Height { get; set; }
        }

        //图标曲线类
        public class AppTimer
        {
            public string time { get; set; }//时间点
            public int length { get; set; }//使用时长
            public AppTimer(string time,int length)
            {
                this.time = time;
                this.length = length;
            }
        }
        public class AllTimer
        {
            public List<AppTimer> appTimersAll { get; set; }
            public List<AppTimer> appTimersAll2 { get; set; }
            public string forTest { get;set; }
            public List<Person> Data { get; set; }
            public bool ifContainTime(List<AppTimer> l,int num)//
            {
                foreach(var app in l)
                {
                    string hh = app.time.Substring(0, 2);
                    DateTime dt = new DateTime(2023, 5, 31, hour: num, minute: 0, second: 0);
                    if(dt.ToString("t").Substring(0, 2) == hh)
                    {
                        return true;//包含该时间点
                    }
                }
                return false;//不包含该时间点
            }

            public AllTimer()
            {
                Data = new List<Person>()
            {
                new Person { Name = "David", Height = 180 },
                new Person { Name = "Michael", Height = 170 },
                new Person { Name = "Steve", Height = 160 },
                new Person { Name = "Joel", Height = 182 }
            };
                //右侧时间折线图
                appTimersAll = new List<AppTimer>();
                var totayAllAppsTime = GlobalData.DataInstance.GetAppDayData(1, DateTime.Now);
                foreach (var timer in totayAllAppsTime)
                {
                    appTimersAll.Add(new AppTimer(timer.DataTime.ToString("t"), timer.Time));
                }
                int p = 0;
                appTimersAll2 = new List<AppTimer>();
                for (int i = 0; i < 24; i++)
                {
                    if (!ifContainTime(appTimersAll, i))
                    {
                        appTimersAll2.Add(new AppTimer(new DateTime(2023, 5, 31,i,0,0).ToString("t"), 0));
                    }
                    else
                    {
                        appTimersAll2.Add(appTimersAll[p]);
                        p++;
                    }
                }
            }
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
            box.SelectedDate = new DateTime(2023, 3, 1);
            //其他数据更改

        }
        private void Week(object sender, RoutedEventArgs e)
        {
            //添加默认起始日期
            box.SelectedDate = new DateTime(2023, 2, 1);
            //其他数据更改

        }

        private void Day(object sender, RoutedEventArgs e)
        {
            //添加默认起始日期
            box.SelectedDate = new DateTime(2023, 1, 1);
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
