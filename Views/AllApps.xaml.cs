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

namespace MvvmTutorials.ToolkitMessages.Views;

/// <summary>
/// Page4.xaml 的交互逻辑
/// </summary>
public partial class AllApps : UserControl
{
    public AppTimeModel appTimeModel = new AppTimeModel();
    public void setDayTimerForApps(DateTime dt)
    {
        List<AppTimer> appTimerList = new List<AppTimer>();
        foreach(var i in DataList)
        {
            if (!i.IsSelected)
                continue;
            var timer = GlobalData.DataInstance.GetProcess(i.Index, dt);
            if(timer!=null)
                appTimerList.Add(new AppTimer(i.Name, timer.Time));
            else
                appTimerList.Add(new AppTimer(i.Name, 0));
        }
        AppTimeModel newAppTimeModel = new AppTimeModel();
        newAppTimeModel.appTimers = appTimerList;
        appTimeModel = newAppTimeModel;
        AllAppsTimer.DataContext = appTimeModel;

    }
    public void OnChecked(object sender, RoutedEventArgs e)
    {
        setDayTimerForApps(DateTime.Now);
        MessageBox.Show(DataList[1].Name + DataList[1].IsSelected.ToString());
    }
    private void CheckBox_Click(object sender, RoutedEventArgs e)
    {
        CheckBox checkBox = sender as CheckBox;
        if (checkBox != null)
        {
            var cntr = Appname.ItemContainerGenerator.ContainerFromIndex(Appname.SelectedIndex); //这里是拿到所选中行
            app selectItem = (app)(cntr as DataGridRow).DataContext; //这里是把选中行转换为对象,进而拿到CheckBox中绑定的名字
            selectItem.IsSelected = !selectItem.IsSelected;//这里是拿到MyList类中的isEnable属性 即上面 xmal中 Checkbox中绑定的变量

            string str = "";
            foreach (var vItem in DataList)
            {
                
                str += vItem.Name;
                str += vItem.IsSelected.ToString() + "\n";
            }
            setDayTimerForApps(DateTime.Now);
        }
    }
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
                IsSelected = true,
                Time=i.TotalTime
            });
        }
        Appname.DataContext = this;
        AllAppsTimer.DataContext = appTimeModel;
        setDayTimerForApps(DateTime.Now);
    }


    public class AppTimeModel
    {
        public List<AppTimer> appTimers { get; set; }
    }
    public class AppTimer
    {
        public AppTimer(string appName, int length)
        {
            this.appName = appName;
            this.length = length;
        }

        public string appName { get; set; }
        public int length { get; set; }
    }

    //文字类
    public class textcombine
    {
        public string text1 { get; set; }
        public string text2 { get; set; }
        public string text3 { get; set; }
        public bool day { get; set; }
        public bool week { get; set; }
        public bool month { get; set; }
    }

    //应用类
    public ObservableCollection<app> DataList { get; } = new ObservableCollection<app>();
    //可考虑迁移
    public class app
    {
        public int time;
        public int Index { get; set; }
        public string ImgPath { get; set; }
        public string Name { get; set; }
        public bool IsSelected { set; get; }
        public int Time
        {
            get => time;
            set
            {

            }
        }

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


