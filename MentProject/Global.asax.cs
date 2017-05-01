using Autofac;
using Autofac.Integration.Mvc;
using MentProject.App_Start;
using MentProject.Controllers;
using MentRepository.RepModel;
using MentRepository.Repository;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MentProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterInstance(new UserRepository()).As<IUserRepository>();            
            builder.RegisterInstance(new RewardRepository()).As<IRewardRepository>();
            builder.RegisterInstance(new LogRepository()).As<ILogRepository>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        private ILogRepository _repository;
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            _repository = new LogRepository();
            if (Request.RequestContext.RouteData.Values["controller"] == null)
                return;
            
            string Controller = (Request.RequestContext.RouteData.Values["controller"] != null ? Request.RequestContext.RouteData.Values["controller"].ToString() : "PanelSpecifications");
            string Action = (Request.RequestContext.RouteData.Values["action"] != null ? Request.RequestContext.RouteData.Values["action"].ToString() : "Index");
            string IP = Request.UserHostAddress;
            string UserName = User.Identity.Name;
            string GUID = Guid.NewGuid().ToString();
            
            AddLogMesg(Controller, Action, IP, UserName, GUID, "StartLog", "StartLog");
            
            foreach (var item in Request.RequestContext.RouteData.Values)
            {
                if (item.Key.Trim().ToLower() != "controller"
                    && item.Key.Trim().ToLower() != "action")
                {
                    AddLogMesg(Controller, Action, IP, UserName, GUID, item.Key ?? "", Convert.ToString(item.Value) ?? "");   
                }
            }

            // Request Query String
            foreach (string key in Request.QueryString.Keys)
            {
                AddLogMesg(Controller, Action, IP, UserName, GUID, key ?? "", Convert.ToString(Request.QueryString[key]) ?? ""); 

            }

            // Request Form Values
            foreach (string key in Request.Form.Keys)
            {
                string Value = Convert.ToString(Request.Form[key]);
                AddLogMesg(Controller, Action, IP, UserName, GUID, key ?? "", Convert.ToString(Request.Form[key]) ?? "");   
            }
        }

        private void AddLogMesg(string Controller, string Action, string IP, string UserName, string GUID, string Field, string Value)
        {
            _repository.Add(new LogRepModel()
            {
                Controller = Controller,
                Action = Action,
                IP = IP,
                UserName = string.IsNullOrEmpty(UserName) ? "anonim" : UserName,
                Field = Field,
                Value = Value,
                GUID = GUID,
                CreateDate = DateTime.Now
            });
        }
    }
}
