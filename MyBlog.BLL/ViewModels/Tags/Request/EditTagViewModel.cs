using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Tags.Request;

/// <summary>
///     Модель обновления тега
/// </summary>
public class EditTagViewModel
{
    public int Id { get; set; }

    [Display(Name = "Название тега")] public string Name { get; set; }
}