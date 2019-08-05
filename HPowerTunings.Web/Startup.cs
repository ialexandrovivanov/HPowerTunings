using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using HPowerTunings.Data;
using HPowerTunings.Web.Core;
using HPowerTunings.Services.Car;
using HPowerTunings.Data.Models;
using System.Threading.Tasks;
using System.Reflection;
using HPowerTunings.Common.Mapping;
using HPowerTunings.Services.Repair;
using HPowerTunings.Services.Appointment;
using HPowerTunings.Services.Client;
using HPowerTunings.Services.Company;
using HPowerTunings.Services.Part;
using HPowerTunings.Services.Supplier;

namespace HPowerTunings.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.ConfigureApplicationCookie(options =>
                    {
                        options.LoginPath = "/Identity/Account/Login";
                        options.LogoutPath = "/Identity/Account/Logout";
                        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                        options.Cookie.Name = "HPowerTuningsCookie";
                        options.Cookie.HttpOnly = true;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                        options.SlidingExpiration = true;
                    });

            services.AddDbContextPool<ApplicationDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                             b => b.MigrationsAssembly("HPowerTunings.Web")));

            //Account cloudinaryCredentials = new Account(
            //   this.Configuration["Cloudinary:CloudName"],
            //   this.Configuration["Cloudinary:ApiKey"],
            //   this.Configuration["Cloudinary:ApiSecret"]);

            //Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            //services.AddSingleton(cloudinaryUtility);

            services.AddIdentity<Client, IdentityRole>(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 3;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.User.RequireUniqueEmail = true;
                        options.SignIn.RequireConfirmedEmail = true;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                        options.Lockout.MaxFailedAccessAttempts = 5;
                        options.Lockout.AllowedForNewUsers = true;
                    })
                    .AddRoles<IdentityRole>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultUI(UIFramework.Bootstrap4)
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<UserManager<Client>, UserManager<Client>>();
            services.AddTransient<SignInManager<Client>, SignInManager<Client>>();
            services.AddTransient<RoleManager<IdentityRole>, RoleManager<IdentityRole>>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IRepairService, RepairService>();
            services.AddTransient<IDayCreator, DayCreator>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<ICustomClientService, CustomClientService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IPartService, PartService>();
            services.AddTransient<ISupplierService, SupplierService>();

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddRazorPagesOptions(options =>
                    {
                        options.AllowAreas = true;
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                    });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ViewModels.ErrorViewModel).GetTypeInfo().Assembly,
                                              typeof(Data.Models.Appointment).GetTypeInfo().Assembly);

            if (env.IsDevelopment() || env.IsEnvironment("QA"))
            {
                var seeder = new DatabaseSeeder(app.ApplicationServices.GetService<ApplicationDbContext>());
                seeder.SeedDatabase().GetAwaiter().GetResult();
                var dayCreator = new DayCreator(app.ApplicationServices.GetService<ApplicationDbContext>());
                Task.Run(() => dayCreator.CreateDay());

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
