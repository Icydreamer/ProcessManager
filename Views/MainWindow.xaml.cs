using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HandyControl.Controls;
using ScottPlot;
namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        HomePage HomePage = new HomePage();
        AllApps AllApps = new AllApps();
        SingleApp SingleApp = new SingleApp();
        Monitor Monitor = new Monitor();
            public MainWindow()
            {
             GlobalUse._Messager.PageContent =HomePage;
            InitializeComponent();
                this.DataContext = GlobalUse._Messager;   
            }
    
        }
    
    public class Messager : INotifyPropertyChanged
    {
        private UserControl pageContent;
        public UserControl PageContent
        {
            get { return pageContent; }
            set
            {
                pageContent = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PageContent"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public Messager()
        {

            PageContent = null;
        }
    }

    public class GlobalUse
    {
        public static Messager _Messager { get; set; }
        static GlobalUse()
        {
            _Messager = new Messager();
        }
    }


}
