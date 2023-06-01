using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;
using MvvmTutorials.ToolkitMessages.Views;

//using MvvmTutorials.ToolkitMessages.Models;

namespace MvvmTutorials.ToolkitMessages.ViewModels;

public partial class MainWindowViewModel: ObservableObject
{

    [ObservableProperty]
    private int totalTime = 0;//总时长

    //页面路径
    [ObservableProperty]
    private string pageSource = "AllApps.xaml";

    //设置部分
    [ObservableProperty]
    private bool autoStart = false;//开机自启
    [ObservableProperty]
    private bool messageBox = true;//消息框
    [ObservableProperty]
    private bool light = false;//程序闪烁

    //app名称
    [ObservableProperty]
    private string appName = "QQ";//要显示APP名称
    [ObservableProperty]
    private string appMostTime = "QQ";//使用最长时间的应用名称

    //使用次数
/*    [ObservableProperty]
    private int times = 0;//当日使用次数
    [ObservableProperty]
    private int timesWeek = 0;//本周累计使用次数
    [ObservableProperty]
    private int timesDayPerWeek = 0;//本周每日平均使用次数*/

    //使用时长
    [ObservableProperty]
    private int time = 0;//当日使用时长
    [ObservableProperty]
    private int timeWeek = 0;//本周累计使用时长
    [ObservableProperty]
    private int timeDayPerWeek = 0;//本周每日平均使用时长

    [ObservableProperty]
    private int lastUse = 0;//距离上次使用已经过去

/*    public RelayCommand ButtonClickCommand { get; }

    public MainWindowViewModel() 
    {
        ButtonClickCommand = new RelayCommand(() => PageSource = "HomePage.xaml");z
    }*/
    [RelayCommand]
    private void ButtonClick()
    {

        PageSource = "HomePage.xaml";
        TotalTime = 100;
    }

    public MainWindowViewModel()
    {

    }
}