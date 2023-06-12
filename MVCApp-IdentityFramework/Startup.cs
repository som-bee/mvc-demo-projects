using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCApp_IdentityFramework.Startup))]
namespace MVCApp_IdentityFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
