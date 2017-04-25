using MentData;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;

namespace MentRepository.Repository
{
    public interface IUserRepository
    {
        UserRepModel GetUserById(long id);
        UsersRepModel GetAllUsers();
        bool SaveUser(UserRepModel user);
        bool DeleteUser(long id);
    }
}