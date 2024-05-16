using FluentValidation;
using MyBlog.BLL.ViewModels.Tags.Request;

namespace MyBlog.BLL.Validators.Tags;

public class CreateTagViewModelValidator : AbstractValidator<CreateTagViewModel>
{
    public CreateTagViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Поле Название обязательно для заполнения!");
    }
}