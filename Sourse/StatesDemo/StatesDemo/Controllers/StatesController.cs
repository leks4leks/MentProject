using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI;

namespace StatesDemo.Controllers
{
    public class StatesController : Controller
    {
		public ActionResult RequestString(int counter = 0)
		{
			ViewBag.Counter = ++counter;
			return View("View");
		}

        public ActionResult Hidden(int counter = 0)
        {
	        ViewBag.Counter = ++counter;
            return View("View");
        }

		public ActionResult AppContext()
		{
			var counterObj = HttpContext.Application["Counter"];
			if (counterObj == null)
			{
				counterObj = 0;
				HttpContext.Application.Add("Counter", counterObj);
			}

			int counter = (int)counterObj;
			HttpContext.Application["Counter"] = ++counter;

			ViewBag.Counter = counter;
			return View("View");
		}

	    public ActionResult Cookie()
	    {
		    var cookie = Request.Cookies["Counter"];
		    if (cookie == null)
		    {
			    cookie = new HttpCookie("Counter", 0.ToString());
				Response.Cookies.Add(cookie);
		    }
		    int counter = Convert.ToInt32(cookie.Value);
		    counter++;
		    cookie.Value = counter.ToString();
		    Response.Cookies["Counter"].Value = counter.ToString();

			ViewBag.Counter = counter;
			return View("View");
	    }

		public ActionResult Sessions()
		{
			var counterObj = Session["Counter"];
			int counter = counterObj == null ? 0 : Convert.ToInt32(counterObj);
			
			//Thread.Sleep(5000);
			counter++;
			Session["Counter"] = counter;

			ViewBag.Counter = counter;
			return View("View");
		}

		//[OutputCache(Duration = 30, Location = OutputCacheLocation.Server, VaryByParam = "Id")]
		public ActionResult Caches()
		{
			var counterObj = HttpContext.Cache["Counter"];
			int counter = counterObj == null ? 0 : Convert.ToInt32(counterObj);

			counter++;
			HttpContext.Cache.Remove("Counter");
			HttpContext.Cache.Add("Counter", counter, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(10), CacheItemPriority.Normal, null);

			ViewBag.Counter = counter;
			return View("View");
		}
    }
}