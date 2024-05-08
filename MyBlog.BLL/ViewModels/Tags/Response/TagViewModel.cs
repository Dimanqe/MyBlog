using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Tags.Response;

/// <summary>
///     Модель одного тега
/// </summary>
public class TagViewModel
{
    public int Id { get; set; }

    [Display(Name = "Name")] public string Name { get; set; }

    public bool IsSelected { get; set; }
}