using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrainingTrackerMVC.Startup))]
namespace TrainingTrackerMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
