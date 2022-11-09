using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cooking_MVC.Startup))]
namespace Cooking_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
