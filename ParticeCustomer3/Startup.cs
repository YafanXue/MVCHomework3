using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParticeCustomer3.Startup))]
namespace ParticeCustomer3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
