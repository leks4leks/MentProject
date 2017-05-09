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
    [HandleError()]
    public class RewardController : Controller
    {
        private IRewardRepository _repository;

        public RewardController(IRewardRepository repository)
        {
            _repository = repository;
        }

        private Rewards LoadRegistry(int userId = 0, string rewName = null)
        {
            var rew = RewardMapper.RewardsRepModelToRewardsMapper(_repository.GetAllRewards(userId, rewName), userId);
            if (Session["Rewards"] != null)
            {
                rew.AddRange(((List<RewardModel>)Session["Rewards"]));
            }
            return rew;
        }
        private Rewards LoadRegistryForLook(int userId = 0)
        {
            return RewardMapper.RewardsRepModelToRewardsMapper(_repository.GetRewardsByUser(userId), userId);
        }

        [Route("awards")]
        public ActionResult Index()
        {
            return View(LoadRegistry());
        }

        [Route("awards/{id:int}")]
        public ActionResult GetRewardById(int id = 0)
        {
            return Edit(id);
        }

        [Route("awards/{rewName}")]
        public ActionResult GetRewardByName(string rewName = null)
        {
            var rews = LoadRegistry(0, rewName);
            if (rews.Count == 1 && !string.IsNullOrEmpty(rewName))
                return View("AddEdit", rews.FirstOrDefault());
            return View("Index", rews);
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
        
        [Route("create-award")]
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

        public ActionResult LookForm(int? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            var rewardModel = LoadRegistryForLook((int)id);

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
                if (User.IsInRole("aspadmin"))
                {
                    if (Session["Rewards"] == null)
                    {
                        List<RewardModel> rews = new List<RewardModel>();
                        rews.Add(rewardModel);
                        Session.Add("Rewards", rews);
                    }
                    else
                    {
                        ((List<RewardModel>)Session["Rewards"]).Add(rewardModel);
                    }
                    
                }
                else
                {
                    _repository.SaveReward(RewardMapper.RewardModelToRewardRepModelMapper(rewardModel));
                }
                return RedirectToAction("Index");
            }

            return View("AddEdit", rewardModel);
        }
        
        [Route("awards/{id:int}/edit")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            RewardModel rewardModel = GetRewardById(id);

            if (rewardModel == null)
                return HttpNotFound();

            return View("AddEdit", rewardModel);
        }

        [Route("awards/{id:int}/delete")]
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