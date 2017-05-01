using MentData;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentProject.Helper
{
    public static class LogRepMapper
    {
        /// <summary>
        /// Log to LogRepModel
        /// </summary>
        /// <param name="user">Log</param>
        /// <returns>LogRepModel</returns>
        public static LogRepModel LogToLogRepModelMapper(Log log)
        {
            return new LogRepModel()
            {
                Action = log.Action,
                Controller = log.Controller,
                CreateDate = log.CreateDate,
                Field = log.Field,
                GUID = log.GUID,
                IP = log.IP, 
                LogID = log.LogID, 
                UserName = log.UserName,
                Value = log.Value
            };
        }

        /// <summary>
        /// LogRepModel to Log
        /// </summary>
        /// <param name="userRepModel">LogRepModel</param>
        /// <returns>Log</returns>
        public static Log LogRepModelToLoglMapper(LogRepModel logRep)
        {
            return new Log()
            {
                Action = logRep.Action,
                Controller = logRep.Controller,
                CreateDate = logRep.CreateDate,
                Field = logRep.Field,
                GUID = logRep.GUID,
                IP = logRep.IP,
                LogID = logRep.LogID,
                UserName = logRep.UserName,
                Value = logRep.Value
            };
        }

        /// <summary>
        /// List Log to List LogRepModel
        /// </summary>
        /// <param name="users">List<Log></param>
        /// <returns>List LogRepModel</returns>
        public static List<LogRepModel> ListLogToListLogRepModelMapper(List<Log> logs)
        {
            var res = new List<LogRepModel>();

            foreach (var item in logs)
                res.Add(LogToLogRepModelMapper(item));

            return res;
        }
    }
}