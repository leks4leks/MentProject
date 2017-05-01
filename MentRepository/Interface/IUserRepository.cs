using MentData;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;

namespace MentRepository.Repository
{
    public interface IUserRepository
    {
        UserRepModel GetUserById(long id);
        UsersRepModel GetAllUsers(string userName);
        bool SaveUser(UserRepModel user);
        bool DeleteUser(long id);
    }
}