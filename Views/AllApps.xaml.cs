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

namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class AllApps : UserControl
    {
        AllAppsRight1 AllAppsRight1 = new AllAppsRight1();
        AllAppsRight2 AllAppsRight2 = new AllAppsRight2();
        public AllApps()
        {
            GlobalUse1._Messager1.RightContent = AllAppsRight1;
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                DataList.Add(new Foo()
                {
                    Index = i,
                    ImgPath= "..\\Resources\\2.png",
                    Name = "lindexi",
                    IsSelected=true,
                    Type="typei",
                    Remark = "doubi"
                });

            }
            DataContext = this;
            this.DataContext = GlobalUse1._Messager1;


        }
        public ObservableCollection<Foo> DataList { get; } = new ObservableCollection<Foo>();
        private void HomePageClick(object sender, RoutedEventArgs e)
        {
            HomePage HomePage = new HomePage();
            GlobalUse._Messager.PageContent = HomePage;
        }
        private void SingleAppClick(object sender, RoutedEventArgs e)
        {
            SingleApp SingleApp = new SingleApp();
            GlobalUse._Messager.PageContent = SingleApp;

        }
        private void MonitorClick(object sender, RoutedEventArgs e)
        {
            Monitor Monitor = new Monitor();
            GlobalUse._Messager.PageContent = Monitor;
        }
        private void SetClick(object sender, RoutedEventArgs e)
        {
            Set window = new Set();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void Right1(object sender, RoutedEventArgs e)
        {
     
            GlobalUse1._Messager1.RightContent = AllAppsRight1;
        }
        private void Right2(object sender, RoutedEventArgs e)
        {

            GlobalUse1._Messager1.RightContent = AllAppsRight2;
        }
    }

   
    public class Foo
    {
        public int Index { get; set; }
        public string ImgPath { get; set; }
        public string Name { get; set; }
        public bool IsSelected { set; get; }
        public string Type { get; set; }
        public string Remark { get; set; }
    }





    //定义消息
    public class Messager1 : INotifyPropertyChanged
    {
        private UserControl rightContent;
        public UserControl RightContent
        {
            get { return rightContent; }
            set
            {
                rightContent = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RightContent"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public Messager1()
        {

            RightContent = null;
        }
    }

    public class GlobalUse1
    {
        public static Messager1 _Messager1 { get; set; }
        static GlobalUse1()
        {
            _Messager1 = new Messager1();
        }
    }

}
