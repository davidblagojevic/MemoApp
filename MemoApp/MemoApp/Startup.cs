using MemoApp.Data;
using MemoApp.Data.Entities;
using MemoApp.Data.Repositories;
using MemoApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MemoApp
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
            //dodajem svoj dbContext
            services.AddDbContext<MemoContext>(options =>
            options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() //mora da se doda rola
                .AddEntityFrameworkStores<ApplicationDbContext>();



            //dodavanje automappera
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //dodajem svoj repository
            services.AddScoped<IMemoRepository, MemoRepository>();

            services.AddScoped<IZoneRepository, ZoneRepository>();

            //adding my service
            services.AddScoped<IMemoService, MemoService>();

            //adding localization to app using Resources
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllersWithViews()
                //adding localization to Views
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                //for data annotations for example dto objects and something like that Required[(ErrorMessage
                //example:Models.MemoViewModel.enUS.resx
                .AddDataAnnotationsLocalization();
            services.AddRazorPages()                //adding localization to Views
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
                //for data annotations for example dto objects and something like that Required[(ErrorMessage
                //example:Models.MemoViewModel.enUS.resx
                .AddDataAnnotationsLocalization();
            //services.AddMvc().AddFormatterMappings(opt =>
            //{
            //    opt.SetMediaTypeMappingForFormat
            //})
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var supportedCultures = new[]
{
                new CultureInfo("en-US"),
                new CultureInfo("fr-FR"),
                new CultureInfo("es")
            };
            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fr-FR"),
                //Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                //UI strings that we have localized
                SupportedUICultures = supportedCultures,
            };
            //setting up a localization middleware to recognize which kind of localization is used
            app.UseRequestLocalization(requestLocalizationOptions);


            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
