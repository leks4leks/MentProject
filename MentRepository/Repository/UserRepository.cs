using MentData;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MentRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            MentDBEntities db = new MentDBEntities();
        }
        private MentDBEntities _db;

        User IUserRepository.GetUserById(long id)
        {
            return _db.Users.Where(_ => _.Id == id && _.IsDeleted == 0).FirstOrDefault();
        }

        List<User> IUserRepository.GetAllUsers()
        {
            return _db.Users.Where(_ => _.IsDeleted == 0).ToList();
        }

        bool IUserRepository.SaveUser(User user)
        {
            _db.Set<User>().AddOrUpdate(user);
            _db.SaveChanges();
            return true;
        }        
    }
}