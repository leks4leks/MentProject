using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MentProject.Models;
using MentProject;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Caching;
using MentProject.Enums;
using MentProject.Helper;

namespace MentProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public ActionResult Index()
        {
            return View(GetAccountsForCheckAdminRole());
        }

        private Accs GetAccountsForCheckAdminRole()
        {            
            var acc = new Accs();
            UserManager.Users.ToList().ForEach(_ => acc.Add(new LoginViewModel() { Login = _.UserName, Id = _.Id, HasAdminRole = _.Roles.Any(r => r.RoleId == RolesInfo.GetRolesIdByName(RolesEnum.admin.ToString())) }));
            return acc;
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginFromCache()
        {
            var accObj = HttpContext.Cache["Acc"];
            LoginViewModel acc = accObj == null ? null : (LoginViewModel)accObj;
            if (acc != null)
            {
                SignInManager.PasswordSignIn(acc.Login, acc.Password, false, shouldLockout: false);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            HttpContext.Cache.Remove("Acc");
            HttpContext.Cache.Add("Acc", model, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(3600), CacheItemPriority.Normal, null);
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var result = await SignInManager.PasswordSignInAsync(model.Login, model.Password, false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Login, Email = Guid.NewGuid().ToString() + "@mail.ru" };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var r = UserManager.FindByName(model.Login);
                    UserManager.AddToRole(r.Id, "user");
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);                    
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteAdminRole(string id)
        {
            if(id != User.Identity.GetUserId())
                UserManager.RemoveFromRole(id, RolesEnum.admin.ToString());
            return View("Index", GetAccountsForCheckAdminRole());
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddAdminRole(string id)
        {
            UserManager.AddToRole(id, RolesEnum.admin.ToString());
            return View("Index", GetAccountsForCheckAdminRole());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.Cache.Remove("Acc");
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}