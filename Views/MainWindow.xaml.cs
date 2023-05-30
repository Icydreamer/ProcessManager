using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HandyControl.Controls;
using ProcessManager;
using ProcessManager.Views;
using ScottPlot;
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
            UserContent = HomePage;
            InitializeComponent();
            this.DataContext = this;
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            UserContent = HomePage;//内容呈现器绑定的UserContent赋值给用户控件1
        }

        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            UserContent = SingleApp;//内容呈现器绑定的UserContent赋值给用户控件2
        }

        private void ButtonClick3(object sender, RoutedEventArgs e)
        {
            UserContent = AllApps;//内容呈现器绑定的UserContent赋值给用户控件3
        }
        private void ButtonClick4(object sender, RoutedEventArgs e)
        {
            UserContent = Monitor;//内容呈现器绑定的UserContent赋值给用户控件4
        }
        private void ButtonClick5(object sender, RoutedEventArgs e)
        {
            UserContent = Set;//内容呈现器绑定的UserContent赋值给用户控件5
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}