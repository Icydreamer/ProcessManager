using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
namespace DataBase.Services
{
    public class DataBase
    {
        private MyDbContext _dbContext;
        private readonly object _locker = new object();
        private bool _locked = false;
        private int _outTime = 10;
        public DataBase(MyDbContext context)
        {
            _dbContext = context;
        }
        public MyDbContext GetReaderContext()
        {
            lock (_locker)
            {
                int lockTime = 0;
                while (_locked)
                {
                    Thread.Sleep(100);
                    lockTime += 100;
                    if (lockTime >= _outTime * 1000)
                    {
                        _locked = false;
                    }
                }
                //_locked = true;

                //_readerContext = new TaiDbContext();
                //return _readerContext;
                return new MyDbContext();
            }
        }
        public MyDbContext GetWriterContext()
        {
            lock (_locker)
            {
                int lockTime = 0;
                while (_locked)
                {
                    Thread.Sleep(100);
                    lockTime += 100;
                    if (lockTime >= _outTime * 1000)
                    {
                        _locked = false;
                    }
                }
                _locked = true;

                _dbContext = new MyDbContext();
                return _dbContext;
            }
        }
        public void CloseWriter()
        {
            _dbContext?.Dispose();
            _locked = false;
        }
    }
}
