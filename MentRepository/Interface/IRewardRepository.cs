using MentData;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;

namespace MentRepository.Repository
{
    public interface IRewardRepository
    {
        RewardRepModel GetRewardById(long id);
        RewardsRepModel GetAllRewards();
        bool SaveReward(RewardRepModel user);
        bool DeleteReward(long id);
    }
}