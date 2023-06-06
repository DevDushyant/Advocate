using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Advocate.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Advocate.Areas.Identity.Data;
using Advocate.Interfaces;
using Advocate.Services;

namespace Advocate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddDbContext<AdvocateContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DbConnection")));

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddTransient<INavigationAsync, AppMenuService>();
            services.AddTransient<ISubjectServiceAsync, SubjectServiceAsync>();
            services.AddTransient<IActTypeServiceAsync, ActTypeServiceAsync>();
            services.AddTransient<IGazzetServiceAsync, GazzetServiceAsync>();
            services.AddTransient<ISubGazzetServiceAsync, SubGazzetServiceAsync>();
            services.AddTransient<IActServiceAsync, ActServiceAsync>();
            services.AddTransient<IFileServiceAsync, FileServiceAsync>();
            services.AddTransient<IBookServiceAsync, BookServiceAsync>();
            services.AddTransient<IRuleServiceAsync, RuleServiceAsync>();
            services.AddTransient<INotifcationTypeAsyncService, NotificationTypeServiceAsync>();
            services.AddTransient<INotificationServiceAsync, NotificationServiceAsync>();
            services.AddRazorPages();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthorization(options=> {
                options.AddPolicy("managerole", policy => policy.RequireRole("Super Admin"));
                options.AddPolicy("managemenu", policy => policy.RequireRole("Super Admin"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Admin}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
