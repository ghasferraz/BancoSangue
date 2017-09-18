using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BancoSangue.Startup))]
namespace BancoSangue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
