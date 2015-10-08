using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlatformCorePrototype.Web.Startup))]
namespace PlatformCorePrototype.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
