using MentProject.Helper;
using MentProject.Models;
using MentRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MentProject.Controllers
{
    [HandleError()]
    public class AspAdminController : Controller
    {
        private IUserRepository _userRepository;
        private IRewardRepository _rewardRepository;

        public AspAdminController(IUserRepository userRepository, IRewardRepository rewardRepository)
        {
            this._userRepository = userRepository;
            this._rewardRepository = rewardRepository;
        }

        // GET: AspAdmin
        public ActionResult Submit()
        {
            var users = (List<UserModel>)Session["Users"];
            if(users != null && users.Count > 0)
                foreach (var item in users)
                {
                    _userRepository.SaveUser(UserMapper.UserModelToUserRepModelMapper(item));
                }
            Session["Users"] = null;

            var rews = (List<RewardModel>)Session["Rewards"];
            if (rews != null && rews.Count > 0)
                foreach (var item in rews)
                {
                    _rewardRepository.SaveReward(RewardMapper.RewardModelToRewardRepModelMapper(item));
                }
            Session["Rewards"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}