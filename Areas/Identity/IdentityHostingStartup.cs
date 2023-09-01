using System;
using DDAC_System.Areas.Identity.Data;
using DDAC_System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DDAC_System.Areas.Identity.IdentityHostingStartup))]
namespace DDAC_System.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DDAC_SystemContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DDAC_SystemContextConnection")));

                services.AddDefaultIdentity<DDAC_SystemUser>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<DDAC_SystemContext>();
            });
        }
    }
}