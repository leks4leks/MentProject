using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatesDemo.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
	        TempData["param"] = "TempData param";
			ViewData["param"] = "Viewdata param";
            return RedirectToAction("Create");
        }

	    public ActionResult Create()
	    {
		    return View();
	    }
    }
}