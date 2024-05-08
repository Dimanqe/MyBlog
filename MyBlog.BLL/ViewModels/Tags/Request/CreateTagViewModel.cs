using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Tags.Request;

/// <summary>
///     Модель создания тега
/// </summary>
public class CreateTagViewModel
{
    public int Id { get; set; }

    [Display(Name = "Название тега")] public string Name { get; set; }
}