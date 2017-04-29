using MentData;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentProject.Helper
{
    public static class RewardRepMapper
    {
        /// <summary>
        /// Reward to RewardRepModel
        /// </summary>
        /// <param name="rew">Reward</param>
        /// <returns>RewardRepModel</returns>
        public static RewardRepModel RewardToRewardRepModelMapper(Reward rew)
        {
            return new RewardRepModel()
            {
                Id = rew.Id,
                Title = rew.Title,
                Description = rew.Description,
                Photo = rew.Photo,
                IsDeleted = rew.IsDeleted
            };
        }

        /// <summary>
        /// RewardRepModel to Reward
        /// </summary>
        /// <param name="rewRepModel">RewardRepModel</param>
        /// <returns>Reward</returns>
        public static Reward RewardRepModelToRewardMapper(RewardRepModel rewRepModel)
        {
            return new Reward()
            {
                Id = rewRepModel.Id,
                Title = rewRepModel.Title,
                Description = rewRepModel.Description,
                Photo = rewRepModel.Photo,
                IsDeleted = rewRepModel.IsDeleted
            };
        }

        /// <summary>
        /// List Reward to RewardsRepModel
        /// </summary>
        /// <param name="rews">List<Reward></param>
        /// <returns>RewardsRepModel</returns>
        public static RewardsRepModel ListRewardToListRewardsRepModelMapper(List<Reward> rews)
        {
            var res = new RewardsRepModel();
            foreach (var item in rews)
                res.Add(RewardToRewardRepModelMapper(item));
            return res;
        }
    }
}