using MentData;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;

namespace MentRepository.Repository
{
    public interface IAccRepository
    {
        AccRepModel GetAcc(string log, string pass);
        AccRepModel Register(AccRepModel entity);
    }
}