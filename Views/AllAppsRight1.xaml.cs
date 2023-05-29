using ScottPlot;
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

namespace ProcessManager.Views
{
    /// <summary>
    /// Page6.xaml 的交互逻辑
    /// </summary>
    public partial class AllAppsRight1 : UserControl
    {
        public AllAppsRight1()
        {
            InitializeComponent();
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
    }
}
