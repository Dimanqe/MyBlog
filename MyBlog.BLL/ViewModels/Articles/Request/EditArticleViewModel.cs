using System.ComponentModel.DataAnnotations;
using MyBlog.BLL.ViewModels.Tags.Response;

namespace MyBlog.BLL.ViewModels.Articles.Request;

/// <summary>
///     Модель обновления статьи
/// </summary>
public class EditArticleViewModel
{
    public int Id { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Заголовок", Prompt = "Заголовок")]
    public string? Title { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Содержание", Prompt = "Содержание")]
    public string? Content { get; set; }

    [Display(Name = "Теги", Prompt = "Теги")]
    public List<TagViewModel>? Tags { get; set; }
}