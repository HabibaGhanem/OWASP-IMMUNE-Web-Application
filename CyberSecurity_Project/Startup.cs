using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CyberSecurity_Project.Startup))]
namespace CyberSecurity_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
