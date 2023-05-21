using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using DataBase.Models;

namespace DataBase.SQLite
{
    public class MyDbContext : DbContext
    {
        /// <summary>
        /// 每日数据
        /// </summary>
        public DbSet<DailyLogModel> DailyLog { get; set; }
        public DbSet<AppModel> App { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public DbSet<CategoryModel> Categorys { get; set; }

        private static string dbFilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Data", "data.db");

        
    }
}
