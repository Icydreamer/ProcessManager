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

    private TaskbarIcon taskbarIcon;
    private bool isWindowVisible = true;

    public MainWindowViewModel()
    {
        // 创建系统托盘图标
        taskbarIcon = new TaskbarIcon();
        taskbarIcon.Icon = new System.Drawing.Icon(".\\Resources\\1.ico"); // 设置图标路径
        taskbarIcon.ToolTipText = "白驹";

        // 添加双击图标事件
        taskbarIcon.TrayMouseDoubleClick += TaskbarIcon_DoubleClick;

        // 添加右键菜单
        taskbarIcon.ContextMenu = new ContextMenu();
        taskbarIcon.ContextMenu.Items.Add(new MenuItem { Header = "主页面", Name = "MainPageMenuItem" });
        taskbarIcon.ContextMenu.Items.Add(new MenuItem { Header = "退出", Name = "ExitMenuItem" });

        // 处理右键菜单项的点击事件
        foreach (MenuItem menuItem in taskbarIcon.ContextMenu.Items)
        {
            menuItem.Click += MenuItem_Click;
        }
        // 处理窗口的 Closing 事件
        Application.Current.MainWindow.Closing += MainWindow_Closing;
    }

    private void TaskbarIcon_DoubleClick(object sender, RoutedEventArgs e)
    {
        ToggleWindowVisibility();
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        MenuItem clickedItem = e.Source as MenuItem;
        if (clickedItem != null)
        {
            if (clickedItem.Name == "MainPageMenuItem")
            {
                ShowWindow();
            }
            else if (clickedItem.Name == "ExitMenuItem")
            {
                taskbarIcon.Dispose();
                Application.Current.Shutdown();
            }
        }
    }

    private void ToggleWindowVisibility()
    {
        if (isWindowVisible)
        {
            HideWindow();
        }
        else
        {
            ShowWindow();
        }
    }

    private void HideWindow()
    {
        Application.Current.MainWindow.Hide();
        isWindowVisible = false;
    }

    private void ShowWindow()
    {
        Application.Current.MainWindow.Show();
        Application.Current.MainWindow.WindowState = WindowState.Normal;
        Application.Current.MainWindow.Activate();
        isWindowVisible = true;
    }

    private void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        // 取消窗口的关闭操作
        e.Cancel = true;
        // 隐藏窗口
        HideWindow();
    }

    // 清理资源
    public void Dispose()
    {
        taskbarIcon.Dispose();
    }

}