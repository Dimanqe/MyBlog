using FluentValidation;
using MyBlog.BLL.ViewModels.Roles.Request;

namespace MyBlog.BLL.Validators.Users;

public class EditRoleViewModelValidator : AbstractValidator<EditRoleViewModel>
{
    public EditRoleViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Поле Название обязательно для заполнения!");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Поле Описание обязательно для заполнения!");
    }
}