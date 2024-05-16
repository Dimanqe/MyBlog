using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.Services;
using AutoMapper;
using MyBlog.DAL.Models.Articles;
using MyBlog.DAL.Models.Comments;
using MyBlog.DAL.Models.Tags;
using MyBlog.DAL.Repositories;
using MyBlog.DAL.Repositories.Interfaces;

namespace MyBlog.App.Extentions
{
    /// <summary>
    /// Методы расширения сервисов
    /// </summary>
    public static class ServiceExtentions
    {
        #region Реализация паттерна UnitOfWork
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddCustomRepository<TEntity, IRepository>(this IServiceCollection services)
                 where TEntity : class
                 where IRepository : class, IRepository<TEntity>
        {
            services.AddScoped<IRepository<TEntity>, IRepository>();

            return services;
        }
        #endregion

        #region Реализация IoC для сервисов
        public static IServiceCollection AddServicesBL(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IDataDefaultService, DataDefaultService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
		#endregion

		#region Реализация IoC для репозиториев
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IRepository<Article>, ArticleRepository>();
			services.AddScoped<IRepository<Comment>, CommentRepository>();
			services.AddScoped<IRepository<Tag>, TagRepository>();

			return services;
		}
		#endregion

        #region Конфигурация automapper'a
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        #endregion
    }
}
