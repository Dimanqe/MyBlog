using FluentValidation;
using MyBlog.BLL.ViewModels.Articles.Request;

namespace MyBlog.BLL.Validators.Users;

public class EditArticleViewModelValidator : AbstractValidator<EditArticleViewModel>
{
    public EditArticleViewModelValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Поле Заголовок обязательно для заполнения!");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Поле Содержание обязательно для заполнения!");
    }
}