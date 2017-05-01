using MentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentRepository.RepModel
{
    public class LogRepModel : Log
    {
        public LogRepModel()
        {
            UserName = "admin";
        }
        public long LogID { get; set; }
        public string UserName { get; set; }
        public string IP { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string GUID { get; set; }
    }
}