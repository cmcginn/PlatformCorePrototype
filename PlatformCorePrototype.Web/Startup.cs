using Microsoft.Owin;
using Owin;
using PlatformCorePrototype.Web;

[assembly: OwinStartup(typeof (Startup))]

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