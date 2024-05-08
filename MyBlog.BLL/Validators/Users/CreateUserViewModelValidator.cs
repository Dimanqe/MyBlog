using FluentValidation;
using MyBlog.BLL.ViewModels.Users.Request;

namespace MyBlog.BLL.Validators.Users;

public class CreateUserViewModelValidator : AbstractValidator<CreateUserViewModel>
{
    public CreateUserViewModelValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty().WithMessage("Поле Имя обязательно для заполнения!");
        RuleFor(x => x.Lastname).NotEmpty().WithMessage("Поле Фамилия обязательно для заполнения!");
        RuleFor(x => x.Login).NotEmpty().WithMessage("Поле Логин обязательно для заполнения!");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Поле Почта обязательно для заполнения!");
        RuleFor(x => x.Password).Length(5).WithMessage("Минимальная длина пароля 5 символов!");
    }
}