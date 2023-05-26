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
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class AllApps : Page
    {
        public AllApps()
        {
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


        }
        public ObservableCollection<Foo> DataList { get; } = new ObservableCollection<Foo>();
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

}
