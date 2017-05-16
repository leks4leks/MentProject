using MentProject.Enums;
using MentProject.Helper;
using MentProject.Models;
using MentRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
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
            var rezRew = new Rewards();
            var rew = RewardMapper.RewardsRepModelToRewardsMapper(_repository.GetAllRewards(userId, rewName), userId);
            if (Session["Rewards"] != null)
            {
                rezRew.AddRange(((List<RewardModel>)Session["Rewards"]));
            }
            rezRew.AddRange(rew.Where(_ => !rezRew.Select(s => s.Id).ToList().Contains(_.Id)));
            return rezRew;
        }

        [Authorize(Roles = "user, admin, aspadmin")]
        private Rewards LoadRegistryForLook(int userId = 0)
        {
            return RewardMapper.RewardsRepModelToRewardsMapper(_repository.GetRewardsByUser(userId), userId);
        }

        [Route("awards")]
        [Authorize(Roles = "admin, aspadmin")]
        public ActionResult Index()
        {
            var model = LoadRegistry();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_adwsListPartial", model);
            }

            return View(model);
        }

        [Route("awards/{id:int}")]
        [Authorize(Roles = "admin, aspadmin")]
        public ActionResult GetRewardById(int id = 0)
        {
            return Edit(id);
        }

        [Route("awards/{rewName}")]
        [Authorize(Roles = "admin, aspadmin")]
        public ActionResult GetRewardByName(string rewName = null)
        {
            var rews = LoadRegistry(0, rewName);
            if (rews.Count == 1 && !string.IsNullOrEmpty(rewName))
                return View("AddEdit", rews.FirstOrDefault());
            return View("Index", rews);
        }

        [Authorize(Roles = "admin, aspadmin")]
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
        [Authorize(Roles = "admin, aspadmin")]
        public ActionResult Create()
        {
            return View("AddEdit", new RewardModel());
        }

        [Authorize(Roles = "admin, aspadmin")]
        public ActionResult SetForm(int? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            var rewardModel = LoadRegistry((int)id);

            if (rewardModel == null)
                return HttpNotFound();

            return View(rewardModel);
        }
        
        [Authorize(Roles = "user, admin, aspadmin")]
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
        [Authorize(Roles = "admin, aspadmin")]
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
        [Authorize(Roles = "admin, aspadmin")]
        public ActionResult AddEdit(HttpPostedFileBase file, [Bind(Include = "Id,Title,Description,Photo,File")] RewardModel rewardModel)
        {
            if (!string.IsNullOrEmpty(rewardModel.Photo))
                this.ModelState.Remove("File");

            if (ModelState.IsValid)
            {
                rewardModel.Photo = FileHelper.SaveFile(file, Server.MapPath("~/Images/"));
                if (User.IsInRole(RolesEnum.aspadmin.ToString()))
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
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_adwsListPartial", rewardModel);
            }

            return RedirectToAction("Index");
        }
        
        [Route("awards/{id:int}/edit")]
        [Authorize(Roles = "admin, aspadmin")]
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
        [Authorize(Roles = "admin, aspadmin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            RewardModel rewardModel = GetRewardById(id);

            if (rewardModel == null)
                return HttpNotFound();
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("_deletePartial", rewardModel);
            }

            return View(rewardModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, aspadmin")]
        public ActionResult DeleteConfirmed(long id)
        {
            _repository.DeleteReward(id);
            return Index();
        }        
    }
}