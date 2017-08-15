using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentalCar.Startup))]
namespace RentalCar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
