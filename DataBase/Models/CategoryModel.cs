using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    /// <summary>
    /// 分类模型
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类图标路径
        /// </summary>
        public string IconFile { get; set; }

    }
}
