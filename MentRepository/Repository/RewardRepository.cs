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

        RewardsRepModel IRewardRepository.GetAllRewards(long userId, string rewName)
        {
            var model = RewardRepMapper.ListRewardToListRewardsRepModelMapper(_db.Rewards.Where(_ => _.IsDeleted == 0).ToList());
            if (userId != 0)
            {
                var userRedards = _db.UserInRewards.Where(_ => _.UserId == userId && _.IsDeleted == 0).Select(_ => _.RewardId).ToList();
                foreach (var item in model)
                {
                    if (userRedards.Contains(item.Id))
                        item.IsSetRewardForUser = true;
                }
            }
            if (!string.IsNullOrEmpty(rewName))
            {
                var rName = rewName.Split('_').ToList();
                if (rName.Count > 1)
                    return RewardRepMapper.ListRewardToListRewardsRepModelMapper(_db.Rewards.Where(_ => _.IsDeleted == 0 && _.Title == rewName).ToList(), true);
                else
                    return RewardRepMapper.ListRewardToListRewardsRepModelMapper(_db.Rewards.Where(_ => _.IsDeleted == 0 && _.Title.Contains(rewName)).ToList());
            }
            return model;
        }

        bool IRewardRepository.SaveUserInReward(Dictionary<int, int> rew)
        {
            var userId = rew.FirstOrDefault();
            var oldRew = _db.UserInRewards.Where(_ => _.UserId == userId.Value).ToList();
            if(oldRew.Count > 0)
                oldRew.ForEach(_ => _.IsDeleted = 1);
            foreach (var item in rew)
                _db.Set<UserInReward>().AddOrUpdate(new UserInReward { RewardId = item.Key, UserId = item.Value });
            _db.SaveChanges();
            return true;
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