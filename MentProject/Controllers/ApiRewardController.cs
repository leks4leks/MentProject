using MentData;
using MentProject.Enums;
using MentProject.Helper;
using MentProject.Models;
using MentRepository.RepModel;
using MentRepository.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;

namespace MentProject.Controllers
{
    public class ApiRewardController : ApiController
    {
        private IRewardRepository _repository;
    
        private MentDBEntities _db;
        public ApiRewardController()
        {
            _repository = new RewardRepository();
            _db = new MentDBEntities();
        }

        private IQueryable<Reward> LoadRegistry()
        {            
            return _db.Rewards.Where(_ => _.IsDeleted == 0);
        }

        [EnableQuery]
        [HttpGet]
        public IQueryable<Reward> Get()
        {   
            return LoadRegistry();
        }

        [HttpPost]
        [Authorize(Roles = "admin, aspadmin")]
        public bool Post(FormDataCollection formData)
        {
            //if (!string.IsNullOrEmpty(rewardModel.Photo))
            //    this.ModelState.Remove("File");

            //if (ModelState.IsValid)
            //{
            //    _repository.SaveReward(RewardMapper.RewardModelToRewardRepModelMapper(rewardModel));
            //}
            //var value1 = Request["SimpleProp1"];
            var value = formData["File"];

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["File"];

                if (httpPostedFile != null)
                {
                    // Get the complete file path
                    String fileSavePath = HttpContext.Current.Server.MapPath("~/Images/") + (Guid.NewGuid()).ToString() + "." + HttpContext.Current.Request.Form["FileName"].Split('.').ToList().Last();

                    // Save the uploaded file to "UploadedFiles" folder
                    httpPostedFile.SaveAs(fileSavePath);
                }
            }

            return true;
        }

        [HttpPut]
        [Authorize(Roles = "admin, aspadmin")]
        public bool Put(HttpPostedFileBase file, RewardModel rewardModel)
        {
            if (!string.IsNullOrEmpty(rewardModel.Photo))
                this.ModelState.Remove("File");

            if (ModelState.IsValid)
            {
                //rewardModel.Photo = FileHelper.SaveFile(file, Server.MapPath("~/Images/"));
                //if (User.IsInRole(RolesEnum.aspadmin.ToString()))
                //{
                //    if (Session["Rewards"] == null)
                //    {
                //        List<RewardModel> rews = new List<RewardModel>();
                //        rews.Add(rewardModel);
                //        Session.Add("Rewards", rews);
                //    }
                //    else
                //    {
                //        ((List<RewardModel>)Session["Rewards"]).Add(rewardModel);
                //    }

                //}
                //else
                //{
                _repository.SaveReward(RewardMapper.RewardModelToRewardRepModelMapper(rewardModel));
                //}
            }

            return true;
        }
        
        [Authorize(Roles = "admin, aspadmin")]
        public bool Delete(long id)
        {
            _repository.DeleteReward(id);
            return true;
        }        
    }
}