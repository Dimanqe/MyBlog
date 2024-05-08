using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Comments.Request;

/// <summary>
///     Модель создания комментария
/// </summary>
public class CreateCommentViewModel
{
    public int ArticleId;

    [DataType(DataType.Text)]
    [Display(Name = "Содержание", Prompt = "Содержание")]
    public string? Content { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Автор", Prompt = "Автор")]
    public string? Author { get; set; }
}