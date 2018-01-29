using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExampleApplication.Startup))]
namespace ExampleApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
