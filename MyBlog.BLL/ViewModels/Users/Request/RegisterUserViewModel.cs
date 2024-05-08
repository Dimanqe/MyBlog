using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Users.Request;

/// <summary>
///     Модель регистрации пользователя
/// </summary>
public class RegisterUserViewModel
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
    [Display(Name = "Email", Prompt = "example@gmail.com")]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Пароль", Prompt = "Введите пароль")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль", Prompt = "Введите пароль")]
    public string? PasswordReg { get; set; }
}