using MentData;
using MentProject.Helper;
using MentRepository.RepModel;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MentRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            _db = new MentDBEntities();
        }
        private MentDBEntities _db;

        UserRepModel IUserRepository.GetUserById(long id)
        {
            return UserRepMapper.UserToUserRepModelMapper(_db.Users.Where(_ => _.Id == id && _.IsDeleted == 0).FirstOrDefault());
        }

        UsersRepModel IUserRepository.GetAllUsers()
        {
            return UserRepMapper.ListUserToListUserRepModelMapper(_db.Users.Where(_ => _.IsDeleted == 0).ToList());
        }

        bool IUserRepository.SaveUser(UserRepModel user)
        {            
            if(user.Id != 0)
            { 
                var dbPhoto = _db.Users.Where(_ => _.Id == user.Id).FirstOrDefault().Photo;
                if (!string.IsNullOrEmpty(dbPhoto) && string.IsNullOrEmpty(user.Photo))
                    user.Photo = dbPhoto;
            }

            _db.Set<User>().AddOrUpdate(UserRepMapper.UserRepModelToUserMapper(user));
            _db.SaveChanges();
            return true;
        }

        bool IUserRepository.DeleteUser(long id)
        {
            var user = _db.Users.Where(_ => _.Id == id).FirstOrDefault();
            if (user != null)
            {
                user.IsDeleted = 1;
                _db.Set<User>().AddOrUpdate(user);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}