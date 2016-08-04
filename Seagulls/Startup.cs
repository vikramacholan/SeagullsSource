using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Seagulls.Startup))]
namespace Seagulls
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
