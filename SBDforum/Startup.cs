using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SBDforum.Startup))]
namespace SBDforum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
