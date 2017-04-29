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
    public class RewardController : Controller
    {
        private IRewardRepository _repository;

        public RewardController(IRewardRepository repository)
        {
            this._repository = repository;
        }

        private Rewards LoadRegistry()
        {
            return RewardMapper.RewardsRepModelToRewardsMapper(_repository.GetAllRewards());
        }

        public ActionResult Index()
        {
            return View(LoadRegistry());
        }

        public ActionResult DeleteReward(long id)
        {
            _repository.DeleteReward(id);
            return View("Index", LoadRegistry());
        }
    }
}