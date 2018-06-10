using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quizzario.Services;
using Quizzario.Data;
using Quizzario.Data.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace Quizzario
{
    public class Startup
    {
        readonly string _testSecret = null;
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
             options.UseLazyLoadingProxies().
             UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Quizzario.Data")));
            
            //mappers
            services.AddScoped<BusinessLogic.Abstract.IQuizDTOMapper, BusinessLogic.Mappers.QuizDTOMapper>();
            services.AddScoped<BusinessLogic.Abstract.IApplicationUserDTOMapper, BusinessLogic.Mappers.ApplicationUserDTOMapper>();
                //services.AddScoped<BusinessLogic.Abstract.IApplicationUserEntityMapper, BusinessLogic.Mappers.ApplicationUserEntityMapper>();
            services.AddScoped<BusinessLogic.Abstract.IQuizEntityMapper, BusinessLogic.Mappers.QuizEntityMapper>();
            //services
            services.AddScoped<BusinessLogic.Abstract.IQuizService, BusinessLogic.Services.QuizService>();
            services.AddScoped<BusinessLogic.Abstract.IUserService, BusinessLogic.Services.UserService>();
            //extension MAPpers
            services.AddScoped<Quizzario.Extensions.IQuizDTOMapperFromViewModel, Quizzario.Extensions.DTOMapper>();
            //repos
            services.AddScoped<Data.Abstracts.IQuizRepository,
                Data.Repositories.EFQuizRepository>();
            services.AddScoped<Data.Abstracts.IApplicationUserRepository,
                Data.Repositories.EFApplicationUserRepository>();
            services.AddScoped<Data.Abstracts.IJSONRepository,
                Data.Repositories.JSONRepository>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IPagingInfoService, PagingInfoService>();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
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
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "MyQuizList", 
                    template: "Quizes/MyQuizes",
                    defaults: new { controller = "Quizes", action = "MyQuizes", p = 1}
                    );
                routes.MapRoute(
                    name: "MyQuizListPage", 
                    template: "Quizes/MyQuizes/{p}",
                    defaults: new { controller = "Quizes", action = "MyQuizes" },
                    constraints: new { p = @"\d+" }
                    );
                routes.MapRoute(
                    name: "FavouriteList",
                    template: "Quizes/Favourite",
                    defaults: new { controller = "Quizes", action = "Favourite", p = 1 }
                    );
                routes.MapRoute(
                    name: "FavouriteListPage",
                    template: "Quizes/Favourite/{p}",
                    defaults: new { controller = "Quizes", action = "Favourite" },
                    constraints: new { p = @"\d+" }
                    );
                routes.MapRoute(
                    name: "AssignedList",
                    template: "Quizes/Assigned",
                    defaults: new { controller = "Quizes", action = "Assigned", p = 1 }
                    );
                routes.MapRoute(
                    name: "AssignedListPage",
                    template: "Quizes/Assigned/{p}",
                    defaults: new { controller = "Quizes", action = "Assigned" },
                    constraints: new { p = @"\d+" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
