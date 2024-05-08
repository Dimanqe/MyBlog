using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyBlog.API.Extentions;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.Validators.Users;
using MyBlog.DAL;
using MyBlog.DAL.Models.Roles;
using MyBlog.DAL.Models.Users;

namespace MyBlog.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
            options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MyBlog API",
                Description = "Application program interface for MyBlog"
            });
        });

        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        var dbpath = Path.Combine(projectDirectory + "\\MyBlog\\MyBlog.DAL\\DB\\myblog.db");

        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EditUserViewModelValidator>());
        services.AddDbContext<BlogDbContext>(option => option.UseSqlite($"Data Source={dbpath}"))
            .AddUnitOfWork()
            .AddRepositories()
            .AddServicesBL()
            .AddAutoMapper()
            .AddIdentity<User, Role>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<BlogDbContext>();

        services.AddAuthentication(optionts => optionts.DefaultScheme = "Cookies")
            .AddCookie("Cookies", options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });
        var serviceProvider = services.BuildServiceProvider();
        var dataService = serviceProvider.GetRequiredService<IDataDefaultService>();
        dataService.SeedDefaultData();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBlog v1"));
        }

        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}