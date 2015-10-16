using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OptionsWebsite.Startup))]
namespace OptionsWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
