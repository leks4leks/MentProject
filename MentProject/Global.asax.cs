using Autofac;
using MentProject.App_Start;
using MentRepository.Repository;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MentProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();            
            builder.RegisterInstance(new UserRepository()).As<IUserRepository>();
            var container = builder.Build();
        }
    }
}
