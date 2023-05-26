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

namespace MvvmTutorials.ToolkitMessages.Views
{
    /// <summary>
    /// Page7.xaml 的交互逻辑
    /// </summary>
    public partial class AllAppsRight2 : Page
    {
        public AllAppsRight2()
        {
            InitializeComponent();
            var plt = new ScottPlot.Plot(600, 400);
            double[] values = { 778, 43, 283, 76, 184 };
            string[] labels = { "C#", "JAVA", "Python", "F#", "PHP" };
            var pie = plt.AddPie(values);
            pie.SliceLabels = labels;
            pie.ShowPercentages = true;
            pie.ShowValues = true;
            pie.ShowLabels = true;
            plt.Legend();
            plt.SaveFig("Resources\\pie_showEverything.png");//绘制扇形图
        }
    }
}
