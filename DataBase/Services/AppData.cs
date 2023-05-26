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
        public AppData(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }
        public List<AppModel> GetAllApps()
        {
            return apps;
        }

        public void Load()
        {
            //Debug.WriteLine("加载app开始");
            using (var db = dataBase.GetReaderContext())
            {
                apps = (
                    from app in db.App
                    join c in db.Categorys
                    on app.CategoryID equals c.ID into itdata
                    from n in itdata.DefaultIfEmpty()
                    select app
                    ).ToList()
                    .Select(m => new AppModel
                    {
                        ID = m.ID,
                        Category = m.Category != null ? m.Category : null,
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
        public AppModel GetApp(string name)
        {
            return apps.Where(m => m.Name == name).FirstOrDefault();
        }
        public AppModel GetApp(int id)
        {
            return apps.Where(m => m.ID == id).FirstOrDefault();
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


        public List<AppModel> GetAppsByCategoryID(int categoryID)
        {
            return apps.Where(m => m.CategoryID == categoryID).ToList();
        }
    }
}
