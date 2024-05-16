using MyBlog.DAL;
using Microsoft.EntityFrameworkCore;
using MyBlog.App.Extentions;
using FluentValidation.AspNetCore;
using MyBlog.BLL.Validators.Users;
using MyBlog.App.Extentions.ExceptionHandler;
using MyBlog.DAL;
using MyBlog.DAL.Models.Roles;
using MyBlog.DAL.Models.Users;
using MyBlog.BLL.Services.Interfaces;

namespace MyBlog.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EditUserViewModelValidator>());
            services.AddDbContext<BlogDbContext>(option => option.UseSqlServer(connectionString))
                    .AddUnitOfWork()
				.AddRepositories()
				.AddServicesBL()
                .AddAutoMapper()
                .AddIdentity<User, Role>(opts =>
                {
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<BlogDbContext>();

            services.AddControllersWithViews();
            var serviceProvider = services.BuildServiceProvider();
            var dataService = serviceProvider.GetRequiredService<IDataDefaultService>();
            dataService.SeedDefaultData();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingExtention>();

			if (!env.IsDevelopment())
            {
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithRedirects("/Error/Default?statusCode={0}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
		}
    }
}
