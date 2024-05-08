using FluentValidation;
using MyBlog.BLL.ViewModels.Roles.Response;
using MyBlog.BLL.ViewModels.Users.Request;

namespace MyBlog.BLL.Validators.Users;

public class EditUserViewModelValidator : AbstractValidator<EditUserViewModel>
{
    public EditUserViewModelValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty().WithMessage("Поле Имя обязательно для заполнения!");
        RuleFor(x => x.Lastname).NotEmpty().WithMessage("Поле Фамилия обязательно для заполнения!");
        RuleFor(x => x.Login).NotEmpty().WithMessage("Поле Логин обязательно для заполнения!");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Поле Почта обязательно для заполнения");
        RuleFor(x => x.NewPassword).Length(5).WithMessage("Минимальная длина пароля 5 символов!");
        RuleFor(x => x.Roles).Must(BeValidRoles).WithMessage("Роль не выбрана!");
    }

    private bool BeValidRoles(List<RoleViewModel> roles)
    {
        if (!roles.Any(x => x.IsSelected)) return false;
        return true;
    }
}