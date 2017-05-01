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
    public class UserController : Controller
    {
        private IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            this._repository = repository;
        }

        private Users LoadRegistry()
        {
            return UserMapper.ListUserRepModelToListUserModelMapper(_repository.GetAllUsers());
        }

        public ActionResult Index()
        {
            return View(LoadRegistry());
        }
        

        public ActionResult Details(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            UserModel userModel = GetUserById(id);

            if (userModel == null)
                return HttpNotFound();

            return View(userModel);
        }

        private UserModel GetUserById(long? id)
        {
            return UserMapper.UserRepModelToUserModelMapper(_repository.GetUserById((long)id));
        }

        public ActionResult Create()
        {
            return View("AddEdit",  new UserModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(HttpPostedFileBase file, [Bind(Include = "Id,Name,BDay,Photo")] UserModel userModel)
        {            
            if (ModelState.IsValid)
            {
                userModel.Photo = FileHelper.SaveFile(file, Server.MapPath("~/Images/"));
                _repository.SaveUser(UserMapper.UserModelToUserRepModelMapper(userModel));
                return RedirectToAction("Index");
            }

            return View("AddEdit", userModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            UserModel userModel = GetUserById(id);

            if (userModel == null)
                return HttpNotFound();

            return View("AddEdit", userModel);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            UserModel userModel = GetUserById(id);

            if (userModel == null)
                return HttpNotFound();

            return View(userModel);
        }

        public ActionResult SetRaward(long? id)
        {
            if (id == null)
                return HttpStatus.BadStatus();

            UserModel userModel = GetUserById(id);

            if (userModel == null)
                return HttpNotFound();

            return Redirect(@"\Reward\SetForm\" + id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _repository.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}