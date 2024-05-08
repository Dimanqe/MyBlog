using FluentValidation;
using MyBlog.BLL.ViewModels.Users.Request;

namespace MyBlog.BLL.Validators.Users;

public class RegisterUserViewModelValidator : AbstractValidator<RegisterUserViewModel>
{
    public RegisterUserViewModelValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty().WithMessage("Поле Имя обязательно для заполнения");
        RuleFor(x => x.Lastname).NotEmpty().WithMessage("Поле Фамилия обязательно для заполнения");
        RuleFor(x => x.Login).NotEmpty().WithMessage("Поле Логин обязательно для заполнения");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Поле Почта обязательно для заполнения");
        RuleFor(x => x.Password).Length(5, 50).WithMessage("Lлина пароля от 5 до 50 символов!");
        RuleFor(x => x.PasswordReg).Equal(x => x.Password).WithMessage("Пароли должны совпадать");
    }
}