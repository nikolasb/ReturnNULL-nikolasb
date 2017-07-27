using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EachVoice.Startup))]
namespace EachVoice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
