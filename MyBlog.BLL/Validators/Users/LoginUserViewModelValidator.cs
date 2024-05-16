using FluentValidation;
using MyBlog.BLL.ViewModels.Users.Request;

namespace MyBlog.BLL.Validators.Users;

public class LoginUserViewModelValidator : AbstractValidator<LoginUserViewModel>
{
    public LoginUserViewModelValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Поле Почта обязательно для заполнения");
        RuleFor(x => x.Password).Length(6).WithMessage("Минимальная длина пароля 6 символов!");
    }
}