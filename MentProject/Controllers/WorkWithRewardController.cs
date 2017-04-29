using MentProject.Helper;
using MentProject.Models;
using MentRepository.RepModel;
using MentRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MentProject.Controllers
{
    public class WorkWithRewardController : Controller
    {
        private IRewardRepository _repository;

        public WorkWithRewardController(IRewardRepository repository)
        {
            this._repository = repository;
        }
        public ActionResult Index(long id)
        {
            return View(id != 0 ? RewardMapper.RewardRepModelToRewardModelMapper(_repository.GetRewardById(id)) : new RewardModel());
        }
        public ActionResult Save(HttpPostedFileBase file)
        {
            var FileName = FileHelper.SaveFile(file, Server.MapPath("~/Images/"));
            
            _repository.SaveReward(new RewardRepModel
                                    {
                                        Id = Convert.ToInt32(Request.Form["Id"]),
                                        Title = Request.Form["Title"],
                                        Description = Request.Form["Description"],
                                        Photo = FileName
                                    }
            );            
            
            return Redirect("~/Reward");
        }
    }
}