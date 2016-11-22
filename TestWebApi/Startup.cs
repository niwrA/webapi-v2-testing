using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestWebApi.Startup))]
namespace TestWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
