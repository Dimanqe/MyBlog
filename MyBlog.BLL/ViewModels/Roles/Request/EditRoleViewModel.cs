using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Roles.Request;

/// <summary>
///     Модель обновления роли
/// </summary>
public class EditRoleViewModel
{
    public int Id { get; set; }

    [Display(Name = "Название роли")] public string Name { get; set; }

    [Display(Name = "Описание роли")] public string? Description { get; set; }
}