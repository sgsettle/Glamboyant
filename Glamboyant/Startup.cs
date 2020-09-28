using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Glamboyant.Startup))]
namespace Glamboyant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
