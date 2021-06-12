using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Final_Project_Management_System.Startup))]
namespace Final_Project_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
