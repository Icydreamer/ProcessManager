using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    /// <summary>
    /// 每小时的使用数据
    /// </summary>
    public class HoursLogModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 统计时段（YYYY-MM-dd HH:00:00)
        /// </summary>
        public DateTime DataTime { get; set; }
        /// <summary>
        /// 使用时长（单位：秒）
        /// </summary>
        public int Time { get; set; } = 0;
        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppModelID { get; set; }
        public virtual AppModel AppModel { get; set; }

    }
}
