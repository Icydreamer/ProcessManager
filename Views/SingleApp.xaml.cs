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
using static MvvmTutorials.ToolkitMessages.Views.AllApps;
namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class SingleApp : UserControl
    {
        public bool ifContainTime(List<AppTimer> l, int num)//
        {
            foreach (var app in l)
            {
                string hh = app.time.Substring(0, 2);
                DateTime dt = new DateTime(2023, 5, 31, hour: num, minute: 0, second: 0);
                if (dt.ToString("t").Substring(0, 2) == hh)
                {
                    return true;//包含该时间点
                }
            }
            return false;//不包含该时间点
        }
        public  void Appname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            app emp = Appname.SelectedItem as app;
            //右侧时间折线图
            List<AppTimer> a1 = new List<AppTimer>();
            var totayAllAppsTime = GlobalData.DataInstance.GetAppDayData(emp.Index, DateTime.Now);
            foreach (var timer in totayAllAppsTime)
            {
                a1.Add(new AppTimer(timer.DataTime.ToString("t"), timer.Time));
            }
            int p = 0;
            List<AppTimer> a2 = new List<AppTimer>();
            for (int i = 0; i < 24; i++)
            {
                if (!ifContainTime(a1, i))
                {
                    a2.Add(new AppTimer(new DateTime(2023, 5, 31, i, 0, 0).ToString("t"), 0));
                }
                else
                {
                    a2.Add(a1[p]);
                    p++;
                }
            }
            AllTimer test = new AllTimer();
            test.appTimersAll2 = a2;
            AppTimers.DataContext = test;

            //应用allapps里的app类，可考虑迁移
            AllApps.app Singleapp = new AllApps.app()
            {
                Index = 0,
                ImgPath = emp.ImgPath,
                Name = emp.Name,
                IsSelected = true,
                Time = 100
            };
            //绑定数据
            singleapp.DataContext = Singleapp;
        }
        public SingleApp()
        {
            InitializeComponent();

            //左下角文字数据更改
            textcombine Allappstext = new textcombine();
            Allappstext.text1 = "距离上次使用已经过去60分钟";
            Allappstext.text2 = "本周共使用8个小时";
            Allappstext.text3 = "平均每天使用1个小时";
            Allapptexts.DataContext = Allappstext;

            //左侧应用修改
            var appList = GlobalData.AppDataInstance.GetAllApps();
            foreach (var i in appList)
            {
                DataList.Add(new app()
                {
                    Index = i.ID,
                    ImgPath = i.IconFile,
                    Name = i.Name,
                    IsSelected = false,
                    Time = i.TotalTime
                });
            }
            Appname.DataContext = this;
            //应用allapps里的app类，可考虑迁移
            AllApps.app Singleapp = new AllApps.app()
            {
                Index = 0,
                ImgPath = "..\\Resources\\2.png",
                Name = DataList[0].Name,
                IsSelected = true,
                Time = 100
            };
            //绑定数据
            singleapp.DataContext = Singleapp;

            AllTimer test = new AllTimer();
            AppTimers.DataContext = test;
        }
        public ObservableCollection<AllApps.app> DataList { get; } = new ObservableCollection<AllApps.app>();



        //图标曲线类
        public class AppTimer
        {
            public string time { get; set; }//时间点
            public int length { get; set; }//使用时长
            public AppTimer(string time, int length)
            {
                this.time = time;
                this.length = length;
            }
        }
        public class AllTimer
        {
            public List<AppTimer> appTimersAll { get; set; }
            public List<AppTimer> appTimersAll2 { get; set; }
            public string forTest { get; set; }
            public bool ifContainTime(List<AppTimer> l, int num)//
            {
                foreach (var app in l)
                {
                    string hh = app.time.Substring(0, 2);
                    DateTime dt = new DateTime(2023, 5, 31, hour: num, minute: 0, second: 0);
                    if (dt.ToString("t").Substring(0, 2) == hh)
                    {
                        return true;//包含该时间点
                    }
                }
                return false;//不包含该时间点
            }

            public AllTimer()
            {
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
                        appTimersAll2.Add(new AppTimer(new DateTime(2023, 5, 31, i, 0, 0).ToString("t"), 0));
                    }
                    else
                    {
                        appTimersAll2.Add(appTimersAll[p]);
                        p++;
                    }
                }
            }
        }
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
