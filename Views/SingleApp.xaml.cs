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

namespace ProcessManager
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
                WpfPlot2.Plot.YLabel("时长/小时，次数/次");
                WpfPlot2.Refresh();
            }
            WpfPlot2.Refresh();
            //绘制时间条
            for (int i = 0; i < 10; i++)
        {
            var hour = 6 * i;
            TimeBarDemo.Hotspots.Add(new DateTimeRange(DateTime.Today.AddHours(hour), DateTime.Today.AddHours(hour + 1)));
            TimeBarDemo.Hotspots.Add(new DateTimeRange(DateTime.Today.AddHours(-hour), DateTime.Today.AddHours(-hour + 1)));
        }
            //list
            for (int i = 0; i < 100; i++)
            {
                DataList.Add(new Foo()
                {
                    Index = i,
                    Name = "lindexi",
                    Remark = "doubi"
                });

            }
            DataContext = this;


        }
        public ObservableCollection<Foo> DataList { get; } = new ObservableCollection<Foo>();
    }
    public class Foo
    {
        public int Index { get; set; }
        public string Name { get; set; }

        public string Remark { get; set; }
    }

}

