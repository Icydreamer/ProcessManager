using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProcessManager.Views
{
    /// <summary>
    /// Page5.xaml 的交互逻辑
    /// </summary>
    public partial class Monitor : Page
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
    }
    public class Fool
    {
        public int Index { get; set; }
        public string Name { get; set; }


    }

}
