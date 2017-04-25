using MentData;
using System;
using System.Collections.Generic;

namespace MentRepository.Repository
{
    public interface IUserRepository
    {
        User GetUserById(long id);
        List<User> GetAllUsers();
        bool SaveUser(User user);
    }
}