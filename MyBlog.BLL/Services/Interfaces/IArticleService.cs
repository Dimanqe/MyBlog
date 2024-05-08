using MyBlog.BLL.ViewModels.Articles.Request;
using MyBlog.BLL.ViewModels.Articles.Response;

namespace MyBlog.BLL.Services.Interfaces;

public interface IArticleService
{
    /// <summary>
    ///     Метод создания статьи
    /// </summary>
    Task<CreateArticleViewModel> CreateArticleAsync();

    /// <summary>
    ///     Метод создания статьи
    /// </summary>
    Task<int> CreateArticleAsync(CreateArticleViewModel model);

    /// <summary>
    ///     Метод обновления статьи
    /// </summary>
    Task<EditArticleViewModel> UpdateArticleAsync(int id);

    /// <summary>
    ///     Метод обновления статьи
    /// </summary>
    Task UpdateArticleAsync(EditArticleViewModel model, int id);

    /// <summary>
    ///     Метод удаления статьи
    /// </summary>
    Task DeleteArticleAsync(int id);

    /// <summary>
    ///     Метод получения всех статьи
    /// </summary>
    Task<ArticlesViewModel> GetAllArticlesAsync();

    /// <summary>
    ///     Метод показа статьи
    /// </summary>
    Task<ArticleViewModel> GetArticleAsync(int id);
}