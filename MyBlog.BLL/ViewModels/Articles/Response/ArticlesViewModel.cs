using MyBlog.DAL.Models.Articles;

namespace MyBlog.BLL.ViewModels.Articles.Response;

/// <summary>
///     Модель списка статей
/// </summary>
public class ArticlesViewModel
{
    public List<Article> Articles { get; set; } = new();
}