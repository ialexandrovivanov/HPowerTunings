using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(HPowerTunings.Web.Areas.Identity.IdentityHostingStartup))]
namespace HPowerTunings.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}