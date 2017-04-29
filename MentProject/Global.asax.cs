using Autofac;
using Autofac.Integration.Mvc;
using MentProject.App_Start;
using MentProject.Controllers;
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
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterInstance(new UserRepository()).As<IUserRepository>();            
            builder.RegisterInstance(new RewardRepository()).As<IRewardRepository>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
