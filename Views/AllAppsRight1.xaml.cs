using DataBase.Models;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Page6.xaml 的交互逻辑
    /// </summary>
    public partial class AllAppsRight1 : UserControl
    {
        public AllAppsRight1()
        {
            InitializeComponent();
            //当前时间
            DateTime now=DateTime.Now;
            List<AppModel> appList = GlobalData.AppDataInstance.GetAllApps();
            Vector<Vector<double>> v;
            foreach(var app in appList)
            {
                
                for(int i = 0; i < 7; i++)
                {
                    DateTime start = DateTime.Now.AddDays((-1)*i);
                    DateTime end = start.AddDays((-1)*i);
                    var time
                }
            }
            ///条形图
            double[] dataX = new double[] { 1, 2, 3, 4, 5,6,7 };
            double[] dataY = new double[] { 1, 4, 9, 16, 25,8,9 };
            double[] dataZ = new double[] { 1, 4, 9, 6, 5,9,9 };
            WpfPlot1.Plot.AddScatter(dataX, dataY);
            WpfPlot1.Plot.AddScatter(dataX, dataZ);
            WpfPlot1.Plot.Title("标题");
            WpfPlot1.Plot.XLabel("时间/天");
            WpfPlot1.Plot.YLabel("时长/小时");
            WpfPlot1.Refresh();
        }
    }
}
