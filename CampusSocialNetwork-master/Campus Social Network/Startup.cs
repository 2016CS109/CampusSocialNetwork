using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Campus_Social_Network.Startup))]
namespace Campus_Social_Network
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
