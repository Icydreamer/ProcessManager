using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        public AllTimer testGlobal = new AllTimer();
        public textcombine AllappstextGlobal = new textcombine();
        public int appIndex = 1;
        public bool ifContainTimeByHour(List<AppTimer> l, int num)//
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
        public bool ifContainTimeByDay(List<AppTimer> l, DateTime dt)//
        {
            foreach (var app in l)
            {
                if (app.time == dt.ToString("MM-dd"))
                    return true;
            }
            return false;//不包含该时间点
        }
        public void setDayTimer(int appId)
        {
            //右侧时间折线图
            List<AppTimer> a1 = new List<AppTimer>();
            var totayAllAppsTime = GlobalData.DataInstance.GetAppDayData(appId, DateTime.Now);
            foreach (var timer in totayAllAppsTime)
            {
                a1.Add(new AppTimer(timer.DataTime.ToString("t"), timer.Time));
            }
            int p = 0;
            List<AppTimer> a2 = new List<AppTimer>();
            for (int i = 0; i < 24; i++)
            {
                if (!ifContainTimeByHour(a1, i))
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
            testGlobal = test;
            AppTimers.DataContext = testGlobal;
        }
        public void setWeekTimer(int appId, DateTime dt)
        {
            var weekTimer = GlobalData.DataInstance.GetProcessThisWeekLogList(appId, dt);
            List<AppTimer> a1 = new List<AppTimer>();
            foreach (var day in weekTimer)
            {
                a1.Add(new AppTimer(day.Date.ToString("MM-dd"), day.Time));
            }
            int p = 0;
            List<AppTimer> a2 = new List<AppTimer>();
            for (int i = -6; i < 1; i++)
            {
                DateTime toJudge = dt.AddDays(i);
                if (!ifContainTimeByDay(a1, toJudge))
                {
                    a2.Add(new AppTimer(toJudge.ToString("MM-dd"), 0));
                }
                else
                {
                    a2.Add(a1[p]);
                    p++;
                }
            }
            AllTimer test = new AllTimer();
            test.appTimersAll2 = a2;
            testGlobal = test;
            AppTimers.DataContext = testGlobal;
        }
        public void Appname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            app emp = Appname.SelectedItem as app;
            appIndex = emp.Index;
            //右侧时间折线图
            if (AllappstextGlobal.day)
            {
                setDayTimer(emp.Index);
/*                MessageBox.Show("setDayTimer");*/
            }
            else if (AllappstextGlobal.week)
            {
                setWeekTimer(emp.Index, strTransToDate(box.Text));
/*                MessageBox.Show("setWeekTimer");*/
            }
            else
                //setDayTimer(emp.Index);
                MessageBox.Show("month");

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
            Allappstext.day = true;
            Allappstext.week = false;
            Allappstext.month = false;
            AllappstextGlobal.day = true;
            AllappstextGlobal.week = false;
            AllappstextGlobal.month = false;
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



        //图标曲线类，使用时长为秒
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
            public bool ifContainTimeByHour(List<AppTimer> l, int num)//
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
                    if (!ifContainTimeByHour(appTimersAll, i))
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
            box.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //其他数据更改
            //MessageBox.Show(AllappstextGlobal.day.ToString() + AllappstextGlobal.week.ToString() + AllappstextGlobal.month.ToString());
            AllappstextGlobal.day = false;
            AllappstextGlobal.week = false;
            AllappstextGlobal.month = true;
        }
        private void Week(object sender, RoutedEventArgs e)
        {
            AllappstextGlobal.day = false;
            AllappstextGlobal.week = true;
            AllappstextGlobal.month = false;
            //添加默认起始日期
            box.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //其他数据更改
            /*            var weektimers = GlobalData.DataInstance.GetThisWeeklogList();
                        List<AppTimer> week1 = new List<AppTimer>();
                        foreach (var w in weektimers)
                        {
                            week1.Add(new AppTimer(w.Date.ToString("MM-dd"), w.Time));
                        }
                        AllTimer test = new AllTimer();
                        test.appTimersAll2 = week1;
                        AppTimers.DataContext = test;*/
            setWeekTimer(appIndex, strTransToDate(box.Text));
        }

        private void Day(object sender, RoutedEventArgs e)
        {
            AllappstextGlobal.day = true;
            AllappstextGlobal.week = false;
            AllappstextGlobal.month = false;
            //添加默认起始日期
            box.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //其他数据更改
            setDayTimer(appIndex);

        }
        public class WeekTimer
        {
            public string time { get; set; }
            public int length { get; set; }
            public WeekTimer(string dayName, int time)
            {
                this.time = dayName;
                this.length = time;
            }
        }

        //切换视图按钮
        private void style(object sender, RoutedEventArgs e)
        {
            //更改绘制视图的方法
        }

        public DateTime strTransToDate(string date)//格式为yyyy-MM-dd
        {
            var newDate = BoxStringTran(date);
            return DateTime.ParseExact(newDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        //实例化自定义command：searchcommand!!!
        //textbox相关内容指令！！

        public string BoxStringTran(string date)
        {
            var str = date.Split("/");
            var month = int.Parse(str[1]);
            var day = int.Parse(str[2]);
            string m;
            string d;
            string res;
            if(month <= 9)
            {
                m = "0" + str[1];
            }
            else
            {
                m = str[1];
            }
            if(day <= 9)
            {
                d = "0" + str[2];
            }
            else
            {
                d = str[2];
            }
            res = str[0] + "-" + m + "-" + d;
            return res;
        }
    }
}
