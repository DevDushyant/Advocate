using System;
using Advocate.Areas.Identity.Data;
using Advocate.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Advocate.Areas.Identity.IdentityHostingStartup))]
namespace Advocate.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<AdvocateContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("DbConnection")));

                //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<AdvocateContext>()
                //    .AddDefaultTokenProviders();

                services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<AdvocateContext>()
                    .AddDefaultTokenProviders();


                services.AddSession(Opt =>
                {
                    Opt.IdleTimeout = TimeSpan.FromSeconds(10);
                });

                
            });
        }
    }
}