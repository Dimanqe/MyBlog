using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Roles.Response;

/// <summary>
///     Модель одной роли
/// </summary>
public class RoleViewModel
{
    public int Id { get; set; }

    [Display(Name = "Name")] public string Name { get; set; }

    [Display(Name = "Description")] public string Description { get; set; }

    public bool IsSelected { get; set; }
}