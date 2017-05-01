using MentProject.Helper;
using MentProject.Models;
using MentRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MentProject.Controllers
{
    public class RewardController : Controller
    {
        private IRewardRepository _repository;

        public RewardController(IRewardRepository repository)
        {
            _repository = repository;
        }

        private Rewards LoadRegistry(int userId = 0)
        {
            return RewardMapper.RewardsRepModelToRewardsMapper(_repository.GetAllRewards(userId), userId);
        }

        public ActionResult Index()
        {
            return View(LoadRegistry());
        }
        
        public ActionResult Details(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();
            RewardModel rewardModel = GetRewardById(id);

            if (rewardModel == null)
                return HttpNotFound();

            return View(rewardModel);
        }

        private RewardModel GetRewardById(long? id)
        {
            return RewardMapper.RewardRepModelToRewardModelMapper(_repository.GetRewardById((long)id));
        }

        public ActionResult Create()
        {
            return View("AddEdit", new RewardModel());
        }

        public ActionResult SetForm(int? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            var rewardModel = LoadRegistry((int)id);

            if (rewardModel == null)
                return HttpNotFound();

            return View(rewardModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetRewardForUser(List<RewardModel> rew)
        {
            var sendData = new Dictionary<int, int>();
            foreach (var item in rew.Where(_ => _.IsSetRewardForUser))
                sendData.Add(item.Id, item.UserId);
            _repository.SaveUserInReward(sendData);
            return Redirect(@"\User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(HttpPostedFileBase file, [Bind(Include = "Id,Title,Description,Photo,File")] RewardModel rewardModel)
        {
            if (!string.IsNullOrEmpty(rewardModel.Photo))
                this.ModelState.Remove("File");

            if (ModelState.IsValid)
            {
                rewardModel.Photo = FileHelper.SaveFile(file, Server.MapPath("~/Images/"));
                _repository.SaveReward(RewardMapper.RewardModelToRewardRepModelMapper(rewardModel));
                return RedirectToAction("Index");
            }

            return View("AddEdit", rewardModel);
        }
        
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            RewardModel rewardModel = GetRewardById(id);

            if (rewardModel == null)
                return HttpNotFound();

            return View("AddEdit", rewardModel);
        }
        
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            RewardModel rewardModel = GetRewardById(id);

            if (rewardModel == null)
                return HttpNotFound();

            return View(rewardModel);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _repository.DeleteReward(id);
            return RedirectToAction("Index");
        }        
    }
}