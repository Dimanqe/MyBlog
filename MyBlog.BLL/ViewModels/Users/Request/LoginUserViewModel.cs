using System.ComponentModel.DataAnnotations;

namespace MyBlog.BLL.ViewModels.Users.Request;

/// <summary>
///     Модель авторизации пользователя
/// </summary>
public class LoginUserViewModel
{
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Введите Email")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Пароль", Prompt = "Введите Password")]
    public string Password { get; set; }
}