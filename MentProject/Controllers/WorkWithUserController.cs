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
    public class WorkWithUserController : Controller
    {
        private IUserRepository _repository;

        public WorkWithUserController(IUserRepository repository)
        {
            this._repository = repository;
        }
        public ActionResult Index(long id)
        {
            return View(UserMapper.UserRepModelToUserModelMapper(_repository.GetUserById(id)));
        }
        public ActionResult Save(HttpPostedFileBase file)
        {
            var FileName = FileHelper.SaveFile(file, Server.MapPath("~/Images/"));
            
            _repository.SaveUser(new UserRepModel
                                    {
                                        Id = Convert.ToInt32(Request.Form["Id"]),
                                        Name = Request.Form["Name"],
                                        BDay = Convert.ToDateTime(Request.Form["BDay"]),
                                        Photo = FileName
                                    }
            );            
            
            return Redirect("~/User");
        }
    }
}