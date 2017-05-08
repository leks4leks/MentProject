using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MentProject.Startup))]
namespace MentProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
