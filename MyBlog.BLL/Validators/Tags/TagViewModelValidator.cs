using FluentValidation;
using MyBlog.BLL.ViewModels.Tags.Response;

namespace MyBlog.BLL.Validators.Users;

public class TagViewModelValidator : AbstractValidator<TagViewModel>
{
    public TagViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Поле Название обязательно для заполнения!");
    }
}