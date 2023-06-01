using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using HandyControl.Controls;
using ScottPlot;
using ScottPlot.Styles;
using System.Globalization;
using Hardcodet.Wpf.TaskbarNotification;


namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window, INotifyPropertyChanged
    {
        private HomePage HomePage = new HomePage();//实例化用户控件1
        private SingleApp SingleApp = new SingleApp();//实例化用户控件2
        private AllApps AllApps = new AllApps();
        private Monitor Monitor= new Monitor();
        private Set Set = new Set();
        private TaskbarIcon taskbarIcon;
        private bool isWindowVisible = true;
        public MainWindow()
        {
            colors newcolor = new colors();
            newcolor.YY = -590;
            if (Application.Current.Resources.MergedDictionaries[0].Source.ToString() == "pack://application:,,,/ProcessManager;component/Resources/color.xaml")
            {
                Func1(newcolor);
                newcolor.br1 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                Func2(newcolor);
                newcolor.br1 = new System.Windows.Media.SolidColorBrush(Colors.Ivory);

            }
            UserContent = HomePage;
            InitializeComponent();
            ButtonGroup.DataContext = newcolor;
            this.DataContext = this;

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

        //两个主题
        void Func1(colors newcolor)
        {
            newcolor.br1 = new System.Windows.Media.SolidColorBrush(Colors.White);
            newcolor.br2 = new System.Windows.Media.SolidColorBrush(Colors.White);
            newcolor.br3 = new System.Windows.Media.SolidColorBrush(Colors.White);
            newcolor.br4 = new System.Windows.Media.SolidColorBrush(Colors.White);
            newcolor.br5 = new System.Windows.Media.SolidColorBrush(Colors.White);
   
        }
        void Func2(colors newcolor)
        {
            newcolor.br1 = new System.Windows.Media.SolidColorBrush(Colors.Silver);
            newcolor.br2 = new System.Windows.Media.SolidColorBrush(Colors.Silver);
            newcolor.br3 = new System.Windows.Media.SolidColorBrush(Colors.Silver);
            newcolor.br4 = new System.Windows.Media.SolidColorBrush(Colors.Silver);
            newcolor.br5 = new System.Windows.Media.SolidColorBrush(Colors.Silver);

        }
        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            UserContent = HomePage;//内容呈现器绑定的UserContent赋值给用户控件1
            colors newcolor = new colors();
            if (Application.Current.Resources.MergedDictionaries[0].Source.ToString() == "pack://application:,,,/ProcessManager;component/Resources/color.xaml")
            {
                Func1(newcolor);
                newcolor.br1 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                Func2(newcolor);
                newcolor.br1 = new System.Windows.Media.SolidColorBrush(Colors.Ivory);

            }
            newcolor.YY = -590;
            ButtonGroup.DataContext = newcolor;
        }

        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            UserContent = SingleApp;//内容呈现器绑定的UserContent赋值给用户控件2
            colors newcolor = new colors();
            if (Application.Current.Resources.MergedDictionaries[0].Source.ToString() == "pack://application:,,,/ProcessManager;component/Resources/color.xaml")
            {
                Func1(newcolor);
                newcolor.br2 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                Func2(newcolor);
                newcolor.br2 = new System.Windows.Media.SolidColorBrush(Colors.Ivory);

            }
            newcolor.YY = -510;
            ButtonGroup.DataContext = newcolor;
        }

        private void ButtonClick3(object sender, RoutedEventArgs e)
        {
            UserContent = AllApps;//内容呈现器绑定的UserContent赋值给用户控件3
            colors newcolor = new colors();
            newcolor.YY = -430;
            if (Application.Current.Resources.MergedDictionaries[0].Source.ToString() == "pack://application:,,,/ProcessManager;component/Resources/color.xaml")
            {
                Func1(newcolor);
                newcolor.br3 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                Func2(newcolor);
                newcolor.br3 = new System.Windows.Media.SolidColorBrush(Colors.Ivory);

            }
            ButtonGroup.DataContext = newcolor;
        }
        private void ButtonClick4(object sender, RoutedEventArgs e)
        {
            UserContent = Monitor;//内容呈现器绑定的UserContent赋值给用户控件4
            colors newcolor = new colors();
            newcolor.YY = -350;
            if (Application.Current.Resources.MergedDictionaries[0].Source.ToString() == "pack://application:,,,/ProcessManager;component/Resources/color.xaml")
            {
                Func1(newcolor);
                newcolor.br4 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                Func2(newcolor);
                newcolor.br4 = new System.Windows.Media.SolidColorBrush(Colors.Ivory);

            }
            ButtonGroup.DataContext = newcolor;
        }
        private void ButtonClick5(object sender, RoutedEventArgs e)
        {
            UserContent = Set;//内容呈现器绑定的UserContent赋值给用户控件5
            colors newcolor = new colors();
            newcolor.YY = -50;
            if (Application.Current.Resources.MergedDictionaries[0].Source.ToString() == "pack://application:,,,/ProcessManager;component/Resources/color.xaml")
            {
                Func1(newcolor);
                newcolor.br5 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                Func2(newcolor);
                newcolor.br5 = new System.Windows.Media.SolidColorBrush(Colors.Ivory);

            }
            ButtonGroup.DataContext = newcolor;
        }

        private UserControl _content;
        //内容呈现器绑定到UserContent
        public UserControl UserContent
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("UserContent");
            }
        }
  
        //动态绑定的事件触发
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        //按钮颜色
       public class colors
       {
            public Brush br1 { get; set ; } 
            public Brush br2 { get; set; }
            public Brush br3 { get; set; }
            public Brush br4 { get; set; }
            public Brush br5 { get; set; }
            public int YY { set; get; }
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

}
