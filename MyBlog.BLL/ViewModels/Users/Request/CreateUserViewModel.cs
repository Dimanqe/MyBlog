using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Users.Request;

/// <summary>
///     Модель создания пользователя
/// </summary>
public class CreateUserViewModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Имя", Prompt = "Введите имя")]
    public string? Firstname { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
    public string? Lastname { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Логин", Prompt = "Введите логин")]
    public string? Login { get; set; }

    [EmailAddress]
    [Display(Name = "Почта")]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string? Password { get; set; }
}