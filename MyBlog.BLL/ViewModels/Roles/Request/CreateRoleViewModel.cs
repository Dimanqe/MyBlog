using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Roles.Request;

/// <summary>
///     Модель создания роли
/// </summary>
public class CreateRoleViewModel
{
    [Display(Name = "Название роли")] public string Name { get; set; }

    [Display(Name = "Описание роли")] public string? Description { get; set; }
}