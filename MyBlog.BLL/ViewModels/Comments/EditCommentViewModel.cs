using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Comments;

/// <summary>
///     Модель обновления комментария
/// </summary>
public class EditCommentViewModel
{
    public int Id { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Содержание", Prompt = "Содержание")]
    public string? Content { get; set; }
}