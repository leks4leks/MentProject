using MentData;
using MentProject.Helper;
using MentRepository.RepModel;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MentRepository.Repository
{
    public class RewardRepository : IRewardRepository
    {
        public RewardRepository()
        {
            _db = new MentDBEntities();
        }
        private MentDBEntities _db;

        RewardRepModel IRewardRepository.GetRewardById(long id)
        {
            return RewardRepMapper.RewardToRewardRepModelMapper(_db.Rewards.Where(_ => _.Id == id && _.IsDeleted == 0).FirstOrDefault());
        }

        RewardsRepModel IRewardRepository.GetAllRewards()
        {
            return RewardRepMapper.ListRewardToListRewardsRepModelMapper(_db.Rewards.Where(_ => _.IsDeleted == 0).ToList());
        }

        bool IRewardRepository.SaveReward(RewardRepModel rew)
        {
            if (rew.Id != 0)
            {
                var dbPhoto = _db.Rewards.Where(_ => _.Id == rew.Id).FirstOrDefault().Photo;
                if (!string.IsNullOrEmpty(dbPhoto) && string.IsNullOrEmpty(rew.Photo))
                    rew.Photo = dbPhoto;
            }

            _db.Set<Reward>().AddOrUpdate(RewardRepMapper.RewardRepModelToRewardMapper(rew));
            _db.SaveChanges();
            return true;
        }

        bool IRewardRepository.DeleteReward(long id)
        {
            var rew = _db.Rewards.Where(_ => _.Id == id).FirstOrDefault();
            if (rew != null)
            {
                rew.IsDeleted = 1;
                _db.Set<Reward>().AddOrUpdate(rew);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}