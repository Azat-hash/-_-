using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyFinishProject.Startup))]
namespace MyFinishProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
