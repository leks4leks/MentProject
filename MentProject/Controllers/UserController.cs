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

        public ActionResult Index()
        {
            var tt = _repository.GetAllUsers();
            return View();
        }
    }
}