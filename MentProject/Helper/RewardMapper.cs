using MentProject.Models;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentProject.Helper
{
    public static class RewardMapper
    {
        /// <summary>
        /// RewardRepModel to RewardModel
        /// </summary>
        /// <param name="rewRep">RewardRepModel</param>
        /// <returns>RewardModel</returns>
        public static RewardModel RewardRepModelToRewardModelMapper(RewardRepModel rewRep, int userId = 0)
        {
            return new RewardModel()
            {
                Id = rewRep.Id,
                Title = rewRep.Title,
                Description = rewRep.Description,
                Photo = rewRep.Photo,
                IsDeleted = rewRep.IsDeleted,
                IsSetRewardForUser = rewRep.IsSetRewardForUser,
                UserId = userId
            };
        }

        /// <summary>
        /// RewardModel to RewardRepModel
        /// </summary>
        /// <param name="RewardModel">RewardModel</param>
        /// <returns>RewardRepModel</returns>
        public static RewardRepModel RewardModelToRewardRepModelMapper(RewardModel rew)
        {
            return new RewardRepModel()
            {
                Id = rew.Id,
                Title = rew.Title,
                Description = rew.Description,
                Photo = rew.Photo,
                IsDeleted = rew.IsDeleted,
                IsSetRewardForUser = rew.IsSetRewardForUser
            };
        }

        /// <summary>
        /// RewardsRepModel to Rewards
        /// </summary>
        /// <param name="rewsRep">RewardsRepModel</param>
        /// <returns>Rewards</returns>
        public static Rewards RewardsRepModelToRewardsMapper(RewardsRepModel rewsRep, int userId = 0)
        {
            var res = new Rewards();
            foreach (var item in rewsRep)
                res.Add(RewardRepModelToRewardModelMapper(item, userId));
            return res;
        }
    }
}