using FluentValidation;
using MyBlog.BLL.ViewModels.Roles.Request;

namespace MyBlog.BLL.Validators.Users;

public class CreateRoleViewModelValidator : AbstractValidator<CreateRoleViewModel>
{
    public CreateRoleViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Поле Название обязательно для заполнения!");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Поле Описание обязательно для заполнения!");
    }
}