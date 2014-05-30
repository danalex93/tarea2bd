using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Phoro.Startup))]
namespace Phoro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
