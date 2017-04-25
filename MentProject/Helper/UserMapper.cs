﻿using MentProject.Models;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentProject.Helper
{
    public static class UserMapper
    {
        /// <summary>
        /// User to UserRepModel
        /// </summary>
        /// <param name="user">UserRepModel</param>
        /// <returns>UserModel</returns>
        public static UserModel UserRepModelToUserModelMapper(UserRepModel userRepModel)
        {
            return new UserModel()
            {
                Id = userRepModel.Id,
                BDay = userRepModel.BDay,
                Name = userRepModel.Name,
                Photo = userRepModel.Photo,
                IsDeleted = userRepModel.IsDeleted
            };
        }

        /// <summary>
        /// UserRepModel to User
        /// </summary>
        /// <param name="userRepModel">UserModel</param>
        /// <returns>UserRepModel</returns>
        public static UserRepModel UserModelToUserRepModelMapper(UserModel userModel)
        {
            return new UserRepModel()
            {
                Id = userModel.Id,
                BDay = userModel.BDay,
                Name = userModel.Name,
                Photo = userModel.Photo,
                IsDeleted = userModel.IsDeleted
            };
        }

        /// <summary>
        /// Users to List<UserModel>
        /// </summary>
        /// <param name="users">List<UserRepModel></param>
        /// <returns>List<UserModel></returns>
        public static Users ListUserRepModelToListUserModelMapper(UsersRepModel users)
        {
            var res = new Users();
            foreach (var item in users)
                res.Add(UserRepModelToUserModelMapper(item));
            return res;
        }
    }
}