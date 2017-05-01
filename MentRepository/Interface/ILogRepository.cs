using MentData;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;

namespace MentRepository.Repository
{
    public interface ILogRepository
    {
        List<LogRepModel> GetAllLog();
        void Add(LogRepModel entity);
    }
}