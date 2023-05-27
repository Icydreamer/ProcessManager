using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using DataBase.Models.Data;
using System.IO;
using System.Runtime.CompilerServices;
namespace DataBase.Services
{
    public class Data
    {
        private readonly AppData appData;
        private readonly DataBase dataBase;
        private readonly object setLock = new object();
        private const int ONEDAY = 86400;
        private const int ONEHOUR = 3600;
        public Data(AppData appData, DataBase dataBase)
        {
            this.appData = appData;
            this.dataBase = dataBase;
        }
        /// <summary>
        /// 保存APP使用时长
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="time"></param>
        /// <param name="startDateTime"></param>
        public void SaveAppTime(string processName, int time, DateTime startDateTime)
        {
            //  过滤无效数据
            if (string.IsNullOrEmpty(processName) || time <= 0) return;
            Task.Run(() =>
            {
                try
                {
                    if (startDateTime.Minute == 59 && startDateTime.Second >= 58)
                    {
                        //  当记录开始时间接近59分59秒时划到下一个小时时段
                        startDateTime = startDateTime.AddSeconds(2);
                        startDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, startDateTime.Hour, 0, 0);
                    }
                    using(var db = dataBase.GetWriterContext())
                    {
                        var app = db.App.Where(m=>m.Name == processName).FirstOrDefault();
                        if (app == null)
                        {
                            dataBase.CloseWriter();
                            return;
                        }
                        //  当前时段最大可记录时长
                        int nowHoursMaxDuration = (60 - startDateTime.Minute) * 60;
                        time = time > nowHoursMaxDuration ? nowHoursMaxDuration : time;

                        //  更新app累计时长
                        app.TotalTime += time;

                        // 更新每日数据
                        var dailyLog = db.DailyLog.SingleOrDefault(m => m.Date == startDateTime && m.AppModelID == app.ID);
                        if (dailyLog == null)
                        {
                            // 创建新模型
                            db.DailyLog.Add(new Models.DailyLogModel()
                            {
                                Date = startDateTime.Date,
                                AppModelID = app.ID,
                                Time = time > ONEDAY ? ONEDAY : time,
                            });
                        }
                        else
                        {
                            // 运行时间大于一天
                            if(dailyLog.Time + time > ONEDAY)
                            {
                                // 设置为一整体
                                dailyLog.Time = ONEDAY;
                            }
                            else
                            {
                                // 正常累加运行时间
                                dailyLog.Time += time;
                            }
                        }

                        // 更新时间段数据
                        var newTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, startDateTime.Hour, 0, 0);
                        var hoursLog = db.HoursLog.SingleOrDefault(m => m.DataTime == newTime && m.AppModelID == app.ID);
                        if(hoursLog == null)
                        {
                            // 创建新模型
                            db.HoursLog.Add(new Models.HoursLogModel()
                            {
                                DataTime = newTime,
                                AppModelID = app.ID,
                                Time = time
                            });
                        }
                        else
                        {
                            if (hoursLog.Time + time > ONEHOUR)
                            {
                                hoursLog.Time = ONEHOUR;
                            }
                            else
                            {
                                hoursLog.Time += time;
                            }
                        }
                        db.SaveChanges();
                        dataBase.CloseWriter();
                    }
                }
                catch (Exception ex)
                {

                }
            });
        }
        /// <summary>
        /// 查询指定范围数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<DailyLogModel> GetDateRangelogList(DateTime start, DateTime end, int take = -1, int skip = -1)
        {

            IEnumerable<AppModel> apps = appData.GetAllApps();

            using (var db = dataBase.GetReaderContext())
            {
                var data = db.DailyLog
                .Where(m => m.Date >= start && m.Date <= end && m.AppModelID != 0)
                .GroupBy(m => m.AppModelID)
                .Select(m => new
                {
                    Time = m.Sum(a => a.Time),
                    Date = m.FirstOrDefault().Date,
                    AppID = m.FirstOrDefault().AppModelID
                });
                if (skip > 0 && take > 0)
                {
                    data = data.OrderByDescending(m => m.Time).Skip(skip).Take(take);
                }
                else if (skip > 0)
                {
                    data = data.OrderByDescending(m => m.Time).Skip(skip);
                }
                else if (take > 0)
                {
                    data = data.OrderByDescending(m => m.Time).Take(take);
                }
                else
                {
                    data = data.OrderByDescending(m => m.Time);
                }



                //: db.DailyLog
                //.Where(m => m.Date >= start && m.Date <= end && m.AppModelID != 0)
                //.GroupBy(m => m.AppModelID)
                //.Select(m => new
                //{
                //    Time = m.Sum(a => a.Time),
                //    Date = m.FirstOrDefault().Date,
                //    AppID = m.FirstOrDefault().AppModelID
                //})
                //.OrderByDescending(m => m.Time)
                //.Take(take);

                var res = data
                .ToList()
                .Select(m => new DailyLogModel
                {
                    Time = m.Time,
                    Date = m.Date,
                    AppModelID = m.AppID
                }).ToList();

                foreach (var log in res)
                {
                    log.AppModel = appData.GetApp(log.AppModelID);
                }

                return res;
            }
        }
        /// <summary>
        /// 获取所有进程今天的数据
        /// </summary>
        /// <returns></returns>
        public List<DailyLogModel> GetTodaylogList()
        {
            using (var db = dataBase.GetReaderContext())
            {
                var today = DateTime.Now.Date;
                var res = db.DailyLog.Where(m => m.Date == today && m.AppModelID != 0);
                return res.ToList();
            }
        }
        /// <summary>
        /// 获取所有进程本周的数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DailyLogModel> GetThisWeeklogList()
        {
            DateTime weekStartDate = DateTime.Now, weekEndDate = DateTime.Now;
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                weekStartDate = DateTime.Now.Date;
                weekEndDate = DateTime.Now.Date.AddDays(6);
            }
            else
            {
                int weekNum = (int)DateTime.Now.DayOfWeek;
                if (weekNum == 0)
                {
                    weekNum = 7;
                }
                weekNum -= 1;
                weekStartDate = DateTime.Now.Date.AddDays(-weekNum);
                weekEndDate = weekStartDate.Date.AddDays(6);
            }

            return GetDateRangelogList(weekStartDate, weekEndDate);
        }
        /// <summary>
        /// 获取所有进程上周的数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DailyLogModel> GetLastWeeklogList()
        {
            DateTime weekStartDate = DateTime.Now, weekEndDate = DateTime.Now;

            int weekNum = (int)DateTime.Now.DayOfWeek;
            if (weekNum == 0)
            {
                weekNum = 7;
            }


            weekStartDate = DateTime.Now.Date.AddDays(-6 - weekNum);
            weekEndDate = weekStartDate.AddDays(6);

            return GetDateRangelogList(weekStartDate, weekEndDate);
        }
        /// <summary>
        /// 获取指定进程某天的数据
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public DailyLogModel GetProcess(int AppID, DateTime day)
        {

            using (var db = dataBase.GetReaderContext())
            {
                var res = db.DailyLog.Where(m =>
                    m.AppModelID == AppID
                    && m.Date.Year == day.Year
                    && m.Date.Month == day.Month
                    && m.Date.Day == day.Day);
                if (res != null)
                {
                    return res.FirstOrDefault();
                }
                return null;
            }
        }
        /// <summary>
        /// 获取指定进程某个月的数据
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<DailyLogModel> GetProcessMonthLogList(int AppID, DateTime month)
        {
            using (var db = dataBase.GetReaderContext())
            {

                var res = db.DailyLog.Include(m => m.AppModel).Where(
                m =>
                m.Date.Year == month.Year
                && m.Date.Month == month.Month
                && m.AppModelID == AppID
                );
                if (res != null)
                {
                    return res.ToList();
                }
                return new List<DailyLogModel>();
            }
        }
        public void Clear(int AppID, DateTime month)
        {

            using (var db = dataBase.GetReaderContext())
            {

                db.DailyLog.RemoveRange(
                db.DailyLog.Where(m =>
                m.AppModelID == AppID
                && m.Date.Year == month.Year
                && m.Date.Month == month.Month));

                db.HoursLog.RemoveRange(
                    db.HoursLog.Where(m => m.AppModelID == AppID
                    && m.DataTime.Year == month.Year
                    && m.DataTime.Month == month.Month));

                db.SaveChanges();
            }
        }
        public struct ColumnItemDataModel
        {
            public int Total { get; set; }
            public int AppID { get; set; }
            public DateTime Time { get; set; }

        }
        /// <summary>
        /// 根据AppID获取某日每小时使用时长
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<HoursDataModel> GetAppDayData(int AppID, DateTime date)
        {
            using (var db = dataBase.GetReaderContext())
            {

                var data = db.Database.SqlQuery<ColumnItemDataModel>(FormattableStringFactory.Create("select sum(Time) as Total,AppModelID as AppID,DataTime as Time from HoursLogModels  where AppModelID=" + AppID + " and DataTime>='" + date.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and DataTime<= '" + date.Date.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY AppModelID,DataTime ")).ToArray();


                List<HoursDataModel> list = new List<HoursDataModel>();


                list.Add(new HoursDataModel()
                {
                    AppID = AppID,
                    Values = new double[24]
                });

                var item = list[0];

                for (int i = 0; i < 24; i++)
                {
                    string hours = i < 10 ? "0" + i : i.ToString();
                    var time = date.ToString($"yyyy-MM-dd {hours}:00:00");

                    var log = data.Where(m => m.Time.ToString("yyyy-MM-dd HH:00:00") == time).FirstOrDefault();



                    item.Values[i] = log.Total;
                }
                return list;
            }
        }
        /// <summary>
        /// 根据AppID获取指定时间段每小时使用时长
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<HoursDataModel> GetAppRangeData(int AppID, DateTime start, DateTime end)
        {
            using (var db = dataBase.GetReaderContext())
            {
                //  查出有数据的分类

                var data = db.Database.SqlQuery<ColumnItemDataModel>(FormattableStringFactory.Create("select sum(Time) as Total,AppModelID as AppID,Date as Time from DailyLogModels where AppModelID=" + AppID + " and Date>='" + start.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and Date<= '" + end.Date.ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY Date ")).ToArray();


                var ts = end - start;
                var days = ts.TotalDays + 1;


                List<HoursDataModel> list = new List<HoursDataModel>();



                list.Add(new HoursDataModel()
                {
                    AppID = AppID,
                    Values = new double[(int)days]
                });


                var item = list[0];

                for (int i = 0; i < days; i++)
                {
                    string day = i < 10 ? "0" + i : i.ToString();
                    var time = start.AddDays(i).ToString($"yyyy-MM-dd 00:00:00");

                    var log = data.Where(m => m.Time.ToString("yyyy-MM-dd 00:00:00") == time).FirstOrDefault();

                    item.Values[i] = log.Total;
                }
                return list;
            }
        }
        /// <summary>
        /// 根据AppID获取某年每小时使用时长
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<HoursDataModel> GetAppYearData(int AppID, DateTime date)
        {
            using (var db = dataBase.GetReaderContext())
            {
                //  查出有数据的分类

                var dateArr = Time.GetYearDate(date);


                var data = db.Database.SqlQuery<ColumnItemDataModel>(FormattableStringFactory.Create("select sum(Time) as Total,AppModelID as AppID,Date as Time from DailyLogModels  where  AppModelID=" + AppID + " and Date>='" + dateArr[0].Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and Date<= '" + dateArr[1].Date.ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY Date")).ToArray();




                List<HoursDataModel> list = new List<HoursDataModel>();


                list.Add(new HoursDataModel()
                {
                    AppID = AppID,
                    Values = new double[12]
                });


                var item = list[0];

                for (int i = 1; i < 13; i++)
                {
                    string month = i < 10 ? "0" + i : i.ToString();
                    var dayArr = Time.GetMonthDate(new DateTime(date.Year, i, 1));

                    //Debug.WriteLine(dayArr);

                    var total = data.Where(m => m.Time >= dayArr[0] && m.Time <= dayArr[1]).Sum(m => m.Total);


                    item.Values[i - 1] = total;
                }
                return list;
            }
        }
        /// <summary>
        /// 清空指定时间范围数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void ClearRange(DateTime start, DateTime end)
        {
            end = new DateTime(end.Year, end.Month, DateTime.DaysInMonth(end.Year, end.Month));

            using (var db = dataBase.GetReaderContext())
            {
                db.Database.ExecuteSql(FormattableStringFactory.Create("delete from DailyLogModels  where Date>='" + start.Date.ToString("yyyy-MM-01 00:00:00") + "' and Date<= '" + end.Date.ToString("yyyy-MM-dd 23:59:59") + "'"));

                db.Database.ExecuteSql(FormattableStringFactory.Create("delete from HoursLogModels  where DataTime>='" + start.Date.ToString("yyyy-MM-01 00:00:00") + "' and DataTime<= '" + end.Date.ToString("yyyy-MM-dd 23:59:59") + "'"));
            }
        }
        /// <summary>
        /// 获取指定日期范围使用应用量
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int GetDateRangeAppCount(DateTime start, DateTime end)
        {
            IEnumerable<AppModel> apps = appData.GetAllApps();

            using (var db = dataBase.GetReaderContext())
            {
                var res = db.DailyLog
                .Where(m => m.Date >= start && m.Date <= end && m.AppModelID != 0)
                .GroupBy(m => m.AppModelID)
                .Count();

                return res;
            }
        }
        /// <summary>
        /// 获取指定时间（小时）所有使用app数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public IEnumerable<HoursLogModel> GetTimeRangelogList(DateTime time)
        {
            time = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
            using (var db = dataBase.GetReaderContext())
            {
                var res = db.HoursLog.Where(m => m.DataTime == time).ToList();

                foreach (var log in res)
                {
                    log.AppModel = appData.GetApp(log.AppModelID);
                }

                return res;
            }
        }
        public struct TimeDataModel
        {
            public int Total { get; set; }
            public DateTime Time { get; set; }
        }
        /// <summary>
        /// 获取指定时间范围内的汇总数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public double[] GetRangeTotalData(DateTime start, DateTime end)
        {
            using (var db = dataBase.GetReaderContext())
            {
                if (start.Date == end.Date)
                {
                    //  获取24小时
                    var data = db.Database.SqlQuery<TimeDataModel>(FormattableStringFactory.Create("select sum(Time) as Total,DataTime as Time from HoursLogModels where  HoursLogModels.DataTime>='" + start.Date.ToString("yyyy-MM-dd 00:00:00") + "' and HoursLogModels.DataTime<= '" + start.Date.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY DataTime ")).ToArray();

                    double[] result = new double[24];
                    for (int i = 0; i < 24; i++)
                    {
                        string hours = i < 10 ? "0" + i : i.ToString();
                        var time = start.ToString($"yyyy-MM-dd {hours}:00:00");
                        var log = data.Where(m => m.Time.ToString("yyyy-MM-dd HH:00:00") == time).FirstOrDefault();
                        result[i] = log.Total;
                    }
                    return result;
                }
                else
                {
                    //  获取日期
                    var ts = end.Date - start.Date;
                    int days = (int)ts.TotalDays + 1;


                    var data = db.Database.SqlQuery<TimeDataModel>(FormattableStringFactory.Create("select sum(Time) as Total,Date as Time from DailyLogModels where  DailyLogModels.Date>='" + start.Date.ToString("yyyy-MM-dd 00:00:00") + "' and DailyLogModels.Date<= '" + end.Date.ToString("yyyy-MM-dd 23:59:59") + "' GROUP BY Date ")).ToArray();

                    double[] result = new double[days];
                    for (int i = 0; i < days; i++)
                    {
                        var time = start.Date.AddDays(i).ToString($"yyyy-MM-dd 00:00:00");
                        var log = data.Where(m => m.Time.ToString("yyyy-MM-dd 00:00:00") == time).FirstOrDefault();
                        result[i] = log.Total;
                    }

                    return result;
                }
            }
        }
        /// <summary>
        /// 获取指定年份按月统计数据
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public double[] GetMonthTotalData(DateTime date)
        {
            using (var db = dataBase.GetReaderContext())
            {
                var dateArr = Time.GetYearDate(date);
                var data = db.Database.SqlQuery<TimeDataModel>(FormattableStringFactory.Create("select sum(Time) as Total,Date as Time from DailyLogModels  where   Date>='" + dateArr[0].Date.ToString("yyyy-MM-dd HH:mm:ss") + "' and Date<= '" + dateArr[1].Date.ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY Date")).ToArray();
                double[] result = new double[12];

                for (int i = 1; i < 13; i++)
                {
                    string month = i < 10 ? "0" + i : i.ToString();
                    var dayArr = Time.GetMonthDate(new DateTime(date.Year, i, 1));
                    var total = data.Where(m => m.Time >= dayArr[0] && m.Time <= dayArr[1]).Sum(m => m.Total);

                    result[i - 1] = total;
                }
                return result;
            }
        }
    }
}
