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