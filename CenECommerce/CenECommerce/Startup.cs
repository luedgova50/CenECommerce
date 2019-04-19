using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CenECommerce.Startup))]
namespace CenECommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
