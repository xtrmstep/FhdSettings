using Microsoft.Owin;
using Owin;
using SettingsService.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace SettingsService.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
