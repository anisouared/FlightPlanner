using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using DataAccess.DB;
using Microsoft.EntityFrameworkCore;
using System.IO;
using BusinessEntities;
using BusinessLogic;
using DataAccess;
using DataAccess.DA;
using DataAccessInterfaces;
using Microsoft.AspNetCore.Identity;
using Core;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace UI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private ILogError _logError;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FlightPlannerDBContext>(options => options.UseSqlServer(_configuration.GetConnectionString("FlightPlannerDBContext"), b => b.MigrationsAssembly("UI")));
            
            services.AddScoped<IAirCraftDA, AirCraftDA>();
            services.AddScoped<IAirPortDA, AirPortDA>();
            services.AddScoped<IFlightDA, FlightDA>();
            services.AddScoped<IUserDA, UserDA>();
            services.AddScoped<IGroupMemberShipDA, GroupMemberShipDA>();

            //services.AddIdentityCore<UserBE>(options => {
            //}).AddEntityFrameworkStores<FlightPlannerDBContext>()
            //.AddUserManager<UserManager<UserBE>>()
            //.AddSignInManager<SignInManager<UserBE>>()
            //.AddDefaultTokenProviders();

            services.AddIdentity<UserBE, GroupMemberShipBE>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<FlightPlannerDBContext>();

            services.AddTransient<IUserStore<UserBE>, UserBL>();
            services.AddTransient<IRoleStore<GroupMemberShipBE>, GroupMemberShipBL>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Home/Index";
                options.LogoutPath = "/User/Logout";
            });

            //services.Configure<CookiePolicyOptions>(options =>
            //{
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddScoped<ILogError>(provider => new LogError(_configuration));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddOptions();
            services.AddSingleton<IConfiguration>(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseIdentity();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateDefaultUser(services).Wait();
        }

        private async Task CreateDefaultUser(IServiceProvider serviceProvider)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<UserBE>>();

            var powerUser = new UserBE
            {
                UserName = _configuration.GetSection("UserSettings")["Login"],
                Email = _configuration.GetSection("UserSettings")["Email"],
                EmailConfirmed = Convert.ToBoolean(_configuration.GetSection("UserSettings")["EmailConfirmed"]),
                PhoneNumberConfirmed = Convert.ToBoolean(_configuration.GetSection("UserSettings")["PhoneNumberConfirmed"]),
                TwoFactorEnabled = Convert.ToBoolean(_configuration.GetSection("UserSettings")["TwoFactorEnabled"]),
                LockoutEnabled = Convert.ToBoolean(_configuration.GetSection("UserSettings")["LockoutEnabled"]),
                AccessFailedCount = Int32.Parse(_configuration.GetSection("UserSettings")["AccessFailedCount"]),
            };

            string UserPassword = _configuration.GetSection("UserSettings")["UserPassword"];
            var user = await UserManager.FindByEmailAsync(_configuration.GetSection("UserSettings")["Email"]);

            if(user == null)
            {
                    var createPowerUser = await UserManager.CreateAsync(powerUser, UserPassword);
                    if (!createPowerUser.Succeeded)
                    {
                        _logError.Log("adding the default user did not succeed");
                    }
            }
        }
    }
}
