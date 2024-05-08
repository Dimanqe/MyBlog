using System.ComponentModel.DataAnnotations;
using MyBlog.BLL.ViewModels.Roles.Response;

namespace MyBlog.BLL.ViewModels.Users.Request;

/// <summary>
///     Модель обновления пользователя
/// </summary>
public class EditUserViewModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Имя", Prompt = "Имя")]
    public string? Firstname { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Фамилия", Prompt = "Фамилия")]
    public string? Lastname { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Логин", Prompt = "Логин")]
    public string? Login { get; set; }

    [EmailAddress]
    [Display(Name = "Почта")]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Новый пароль")]
    public string? NewPassword { get; set; }

    [Display(Name = "Роли")] public List<RoleViewModel>? Roles { get; set; }

    public int Id { get; set; }
}