using MentData;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentProject.Helper
{
    public static class UserRepMapper
    {
        /// <summary>
        /// User to UserRepModel
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>UserRepModel</returns>
        public static UserRepModel UserToUserRepModelMapper(User user)
        {
            return new UserRepModel()
            {
                Id = user.Id,
                BDay = user.BDay,
                Name = user.Name,
                Photo = user.Photo,
                IsDeleted = user.IsDeleted
            };
        }

        /// <summary>
        /// UserRepModel to User
        /// </summary>
        /// <param name="userRepModel">UserRepModel</param>
        /// <returns>User</returns>
        public static User UserRepModelToUserMapper(UserRepModel userRepModel)
        {
            return new User()
            {
                Id = userRepModel.Id,
                BDay = userRepModel.BDay,
                Name = userRepModel.Name,
                Photo = userRepModel.Photo,
                IsDeleted = userRepModel.IsDeleted
            };
        }

        /// <summary>
        /// List User to UsersRepModel
        /// </summary>
        /// <param name="users">List<User></param>
        /// <returns>UsersRepModel</returns>
        public static UsersRepModel ListUserToListUserRepModelMapper(List<User> users, bool onlyOne = false)
        {
            var res = new UsersRepModel();
            if (onlyOne)
            {
                res.Add(UserToUserRepModelMapper(users.FirstOrDefault()));
                return res;
            }

            foreach (var item in users)
                res.Add(UserToUserRepModelMapper(item));

            return res;
        }
    }
}