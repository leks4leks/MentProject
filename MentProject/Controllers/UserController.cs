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

        public ActionResult DeleteUser(long id)
        {
            _repository.DeleteUser(id);
            return View("Index", LoadRegistry());
        }
    }
}