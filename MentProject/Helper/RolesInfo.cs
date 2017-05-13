using MentProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentProject.Helper
{
    public static class RolesInfo
    {
        public static string GetRolesIdByName(string roleName)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Roles.Where(_ => _.Name == roleName).FirstOrDefault().Id.ToString();
        }
    }
}