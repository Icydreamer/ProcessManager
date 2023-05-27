using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Models;
using DataBase.SQLite;
namespace DataBase.Services
{
    /// <summary>
    /// 从日志写入数据库类
    /// </summary>
    public class LogProcessor
    {
        private readonly MyDbContext DbContext;
        public LogProcessor()
        {
            DbContext = new MyDbContext();
        }

    }
}
