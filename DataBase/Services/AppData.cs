using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
namespace DataBase.Services
{
    public class AppData
    {
        private List<AppModel> apps;
        private readonly DataBase dataBase;
        private readonly object _locker = new object();
        public AppData(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }
        /// <summary>
        /// 加载已存储的app列表，仅建议在启动时调用一次，无必要请勿再次调用
        /// </summary>
        public void Load()
        {
            using (var db = dataBase.GetReaderContext())
            {
                apps = db.App.ToList()
                    .Select(m => new AppModel
                    {
                        ID = m.ID,
                        CategoryID = m.CategoryID,
                        Description = m.Description,
                        File = m.File,
                        IconFile = m.IconFile,
                        Name = m.Name,
                        TotalTime = m.TotalTime
                    })
                    .ToList();
            }
        }

        /// <summary>
        /// 更新app数据，要先调用GetApp获得后更改并传回才有效
        /// </summary>
        /// <param name="app"></param>
        public void UpdateApp(AppModel app)
        {
            using (var db = dataBase.GetWriterContext())
            {
                db.Entry(app).State = EntityState.Modified;
                db.SaveChanges();
                dataBase.CloseWriter();
            }
        }
        /// <summary>
        /// 通过进程名称获取app
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AppModel? GetApp(string name)
        {
            lock (_locker)
            {
                return apps.Where(m => m.Name == name).FirstOrDefault();
            }
        }
        /// <summary>
        /// 通过进程ID获取app
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AppModel? GetApp(int id)
        {
            lock (_locker)
            {
                return apps.Where(m => m.ID == id).FirstOrDefault();
            }
        }
        /// <summary>
        /// 获取所有app
        /// </summary>
        /// <returns></returns>
        public List<AppModel> GetAllApps()
        {
            return apps;
        }
        public void AddApp(AppModel app)
        {
            if (apps.Where(m => m.Name == app.Name).Any())
            {
                return;
            }

            using (var db = dataBase.GetWriterContext())
            {
                db.App.Add(app);
                int res = db.SaveChanges();
                if (res > 0)
                {
                    apps.Add(app);
                }
                dataBase.CloseWriter();
            }
        }
    }
}
