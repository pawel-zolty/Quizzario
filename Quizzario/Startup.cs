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
            services.AddScoped<BusinessLogic.Abstracts.IQuizDTOFactory, BusinessLogic.Factories.QuizDTOFactory>();
            services.AddScoped<BusinessLogic.Abstracts.IApplicationUserDTOFactory, BusinessLogic.Factories.ApplicationUserDTOFactory>();

            //services
            services.AddScoped<BusinessLogic.Abstract.IQuizService, BusinessLogic.Services.QuizService>();

            //repos
            services.AddScoped<Data.Abstracts.IQuizRepository,
                Data.Repositories.EFQuizRepository>();
            services.AddScoped<Data.Abstracts.IApplicationUserRepository,
                Data.Repositories.EFApplicationUserRepository>();
            services.AddScoped<Data.Abstracts.IAssignedRepository,
                Data.Repositories.EFAssignedRepository>();

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
                    name: "MyQuizList", 
                    template: "Quiz/List",
                    defaults: new { controller = "Quiz", action = "List", p = 1}
                    );
                routes.MapRoute(
                    name: "MyQuizListPage", 
                    template: "Quiz/List/Page{p}",
                    defaults: new { controller = "Quiz", action = "List"  },
                    constraints: new { p = @"\d+" }
                    );
                routes.MapRoute(
                    name: "FavouriteList",
                    template: "Quiz/Favourite",
                    defaults: new { controller = "Quiz", action = "Favourite", p = 1 }
                    );
                routes.MapRoute(
                    name: "FavouriteListPage",
                    template: "Quiz/Favourite/Page{p}",
                    defaults: new { controller = "Quiz", action = "Favourite" },
                    constraints: new { p = @"\d+" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
