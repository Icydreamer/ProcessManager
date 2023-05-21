using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    /// <summary>
    /// 每日使用数据
    /// </summary>
    public class DailyLogModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 使用时长（单位：秒）
        /// </summary>
        public int Time { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppModelID { get; set; }

        public virtual AppModel AppModel { get; set; }
    }
}
