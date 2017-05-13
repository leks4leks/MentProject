using MentProject.Helper;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MentProject.Startup))]
namespace MentProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DateBaseSet.FirstFill(new Models.ApplicationDbContext());
            ConfigureAuth(app);
        }
    }
}
