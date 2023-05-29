using System;
using System.Collections.Generic;
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
using MvvmTutorials.ToolkitMessages.Views;

namespace ProcessManager.Views
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UserControl
    {
        public delegate void SendMessage(string value);
        public SendMessage sendMessage;
        public HomePage()
        {
            InitializeComponent();
        }

        private void ColumnDefinition_Confirmed(object sender, HandyControl.Data.FunctionEventArgs<Color> e)
        {

        }

        private void Button_Completed(object sender, EventArgs e)
        {

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
    }
}
