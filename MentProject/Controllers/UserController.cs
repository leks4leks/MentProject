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
    public class UserController : Controller
    {
        private IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            this._repository = repository;
        }
        
        public ActionResult Index()
        {
            return View(LoadRegistry());
        }

        #region Actions
        [ActionName("GetUserByName")]
        public ActionResult GetUserByName(string userName = null)
        {
            var users = LoadRegistry(userName);
            if (users.Count == 1 && !string.IsNullOrEmpty(userName))
                return View("AddEdit", users.FirstOrDefault());
            return View("Index", users);
        }

        [ActionName("GetUserById")]
        public ActionResult GetUserById(int id = 0)
        {
            return View("AddEdit", GetUserByIdExt(id));
        }

        [ActionName("CreateUser")]
        public ActionResult CreateUser()
        {
            return Create();
        }

        [ActionName("GetUserForEdit")]
        public ActionResult GetUserForEdit(int id = 0)
        {
            return Edit(id);
        }

        [ActionName("GetUserForDelete")]
        public ActionResult GetUserForDelete(int id = 0)
        {
            return Delete(id);
        }
        #endregion
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(HttpPostedFileBase file, [Bind(Include = "Id,Name,BDay,Photo")] UserModel userModel)
        {            
            if (ModelState.IsValid)
            {
                userModel.Photo = FileHelper.SaveFile(file, Server.MapPath("~/Images/"));
                if (User.IsInRole("aspadmin"))
                {
                    if (Session["Users"] == null)
                    {
                        List<UserModel> users = new List<UserModel>();
                        users.Add(userModel);
                        Session.Add("Users", users);
                    }
                    else
                    {
                        ((List<UserModel>)Session["Users"]).Add(userModel);
                    }
                }
                else
                {
                    _repository.SaveUser(UserMapper.UserModelToUserRepModelMapper(userModel));
                }
                return RedirectToAction("Index");
            }

            return View("AddEdit", userModel);
        }
        
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return HttpStatus.BadStatus();

            UserModel userModel = GetUserByIdExt(id);

            if (userModel == null)
                return HttpNotFound();

            return View("AddEdit", userModel);
        }

        public ActionResult Delete(long id = 0)
        {
            if (id == 0)
                return HttpStatus.BadStatus();

            UserModel userModel = GetUserByIdExt(id);

            if (userModel == null)
                return HttpNotFound();

            return View(userModel);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            UserModel userModel = GetUserByIdExt(id);

            if (userModel == null)
                return HttpNotFound();

            return View(userModel);
        }

        public ActionResult Create()
        {
            return View("AddEdit", new UserModel());
        }

        public ActionResult SetRaward(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            UserModel userModel = GetUserByIdExt(id);

            if (userModel == null)
                return HttpNotFound();

            return Redirect(@"\Reward\SetForm\" + id);
        }

        public ActionResult LookRaward(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            UserModel userModel = GetUserByIdExt(id);

            if (userModel == null)
                return HttpNotFound();

            return Redirect(@"\Reward\LookForm\" + id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _repository.DeleteUser(id);
            return RedirectToAction("Index");
        }

        private UserModel GetUserByIdExt(long? id)
        {
            return UserMapper.UserRepModelToUserModelMapper(_repository.GetUserById((long)id));
        }
        private Users LoadRegistry(string userName = null)
        {
            var users = UserMapper.ListUserRepModelToListUserModelMapper(_repository.GetAllUsers(userName));
            if (Session["Users"] != null)
            {
                users.AddRange(((List<UserModel>)Session["Users"]));
            }
            return users;
        }
    }
}