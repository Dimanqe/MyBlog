using System.ComponentModel.DataAnnotations;
using MyBlog.BLL.ViewModels.Tags.Response;

namespace MyBlog.BLL.ViewModels.Articles.Request;

/// <summary>
///     Модель создания статьи
/// </summary>
public class CreateArticleViewModel
{
    public int Id { get; set; }
    public int AuthorId { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Заголовок", Prompt = "Заголовок")]
    public string? Title { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Содержание", Prompt = "Содержание")]
    public string? Content { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Теги", Prompt = "Теги")]
    public List<TagViewModel>? Tags { get; set; }
}