using MvvmTutorials.ToolkitMessages.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
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
    /// Page5.xaml 的交互逻辑
    /// </summary>
    public partial class Monitor : UserControl
    {
        public Monitor()
        {
            InitializeComponent();
            for (int i = 0; i < 100; i++)
            {
                DataList.Add(new Fool()
                {
                    Index = i,
                    Name = "lindexi",

                });

            }
            DataContext = this;





        }
        public ObservableCollection<Fool> DataList { get; } = new ObservableCollection<Fool>();
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
        private void AllAppsClick(object sender, RoutedEventArgs e)
        {

            AllApps AllApps = new AllApps();
            GlobalUse._Messager.PageContent = AllApps;
        }
        private void SetClick(object sender, RoutedEventArgs e)
        {
            Set window = new Set();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }
    }
    public class Fool
    {
        public int Index { get; set; }
        public string Name { get; set; }


    }

}
