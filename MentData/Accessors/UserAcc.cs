using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentData.Accessors
{
    public class UserAcc
    {
        public UserAcc()
        {
            MentDBEntities db = new MentDBEntities();
        }
        private MentDBEntities _db;

        public User GetUserById(long id)
        {
            return _db.Users.Where(_ => _.Id == id && _.IsDeleted == 0).FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {
            return _db.Users.Where(_ => _.IsDeleted == 0).ToList();
        }

        public bool InsertUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return true;
        }

        public bool UpdateUser(User user)
        {
            //_db.Users.Attach(user);
            //var entry = _db.Entry(user);
            //if(entry.Property(e => e.Name))
            //entry.Property(e => e.Name).IsModified = true;
            //_db.SaveChanges();
            return true;
        }

    }
}