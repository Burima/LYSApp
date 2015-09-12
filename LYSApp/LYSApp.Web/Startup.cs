using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LYSApp.Web.Startup))]
namespace LYSApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
