using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quizzario.Services;
using Quizzario.Data;
using Quizzario.Data.Entities;


namespace Quizzario
{
    public class Startup
    {
        string _testSecret = null;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

      


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Quizzario.Data")));
            // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            //factories
            services.AddScoped<Data.Abstracts.IQuizDTOFactory, Data.Factories.QuizDTOFactory>();
            services.AddScoped<Data.Abstracts.IApplicationUserDTOFactory, Data.Factories.ApplicationUserDTOFactory>();

            //services
            services.AddScoped<BusinessLogic.Abstract.IQuizService, BusinessLogic.Services.QuizService>();

            //repos
            services.AddScoped<Data.Abstracts.IRepository<Data.Entities.Quiz>,
                Data.Repositories.EFQuizRepository>();
            services.AddScoped<Data.Abstracts.IRepository<Data.Entities.ApplicationUser>,
                Data.Repositories.EFApplicationUserRepository>();

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IPagingInfoService, PagingInfoService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
