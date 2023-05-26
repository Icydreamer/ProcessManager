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
using HandyControl.Data;

namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class SingleApp : Page
    {
        public SingleApp()
        {
            InitializeComponent();
            if (WpfPlot2 != null)
            {
                ///条形图
                double[] dataX = new double[] { 1, 2, 3, 4, 5 };
                double[] dataY = new double[] { 1, 4, 9, 16, 25 };
                double[] dataZ = new double[] { 1, 4, 9, 6, 5 };
                WpfPlot2.Plot.AddScatter(dataX, dataY);
                WpfPlot2.Plot.AddScatter(dataX, dataZ);
                WpfPlot2.Plot.Title("标题");
                WpfPlot2.Plot.XLabel("时间/天");
                WpfPlot2.Plot.YLabel("时长/小时");
                WpfPlot2.Refresh();
            }
            WpfPlot2.Refresh();
        
            //list
            for (int i = 0; i < 100; i++)
            {
                DataList.Add(new Foo2()
                {
                    Index = i,
                    Name = "lindexi",
                    Remark = "doubi"
                });

            }
            DataContext = this;


        }
        public ObservableCollection<Foo2> DataList { get; } = new ObservableCollection<Foo2>();
    }
    public class Foo2
    {
        public int Index { get; set; }
        public string Name { get; set; }

        public string Remark { get; set; }
    }

}

