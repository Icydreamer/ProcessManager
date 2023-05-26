using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models.Data
{
    public class HoursDataModel
    {
        public int AppID { get; set; }
        public int CategoryID { get; set; }
        public double[] Values { get; set; }
    }
}
