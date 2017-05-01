using MentData;
using MentProject.Helper;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace MentRepository.Repository
{
    public class LogRepository : ILogRepository
    {
        public LogRepository()
        {
            _db = new MentDBEntities();
        }
        private MentDBEntities _db;

        List<LogRepModel> ILogRepository.GetAllLog()
        {
            return LogRepMapper.ListLogToListLogRepModelMapper(_db.Logs.ToList());             
        }

        void ILogRepository.Add(LogRepModel entity)
        {
            _db.Set<Log>().AddOrUpdate(LogRepMapper.LogRepModelToLoglMapper(entity));
            _db.SaveChanges();
        }
    }
}