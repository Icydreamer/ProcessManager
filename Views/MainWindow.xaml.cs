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
        public MainWindow()
            {
            colors newcolor = new colors()
            {
                br1 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue),
                br2 = new System.Windows.Media.SolidColorBrush(Colors.White),
                br3 = new System.Windows.Media.SolidColorBrush(Colors.White),
                br4 = new System.Windows.Media.SolidColorBrush(Colors.White),
                br5 = new System.Windows.Media.SolidColorBrush(Colors.White),
                YY=-590
            };
            UserContent = HomePage;
            InitializeComponent();
            ButtonGroup.DataContext = newcolor;
            this.DataContext = this;
       
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            UserContent = HomePage;//内容呈现器绑定的UserContent赋值给用户控件1
            colors newcolor = new colors();
            newcolor.YY = -590;
            newcolor.br1 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            ButtonGroup.DataContext = newcolor;
        }

        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            UserContent = SingleApp;//内容呈现器绑定的UserContent赋值给用户控件2
            colors newcolor = new colors();
            newcolor.YY = -510;
            newcolor.br2 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            ButtonGroup.DataContext = newcolor;
        }

        private void ButtonClick3(object sender, RoutedEventArgs e)
        {
            UserContent = AllApps;//内容呈现器绑定的UserContent赋值给用户控件3
            colors newcolor = new colors();
            newcolor.YY = -430;
            newcolor.br3 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            ButtonGroup.DataContext = newcolor;
        }
        private void ButtonClick4(object sender, RoutedEventArgs e)
        {
            UserContent = Monitor;//内容呈现器绑定的UserContent赋值给用户控件4
            colors newcolor = new colors();
            newcolor.YY = -350;
            newcolor.br4 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
            ButtonGroup.DataContext = newcolor;
        }
        private void ButtonClick5(object sender, RoutedEventArgs e)
        {
            UserContent = Set;//内容呈现器绑定的UserContent赋值给用户控件5
            colors newcolor = new colors();
            newcolor.YY = -50;
            newcolor.br5 = new System.Windows.Media.SolidColorBrush(Colors.AliceBlue);
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


    }

}
