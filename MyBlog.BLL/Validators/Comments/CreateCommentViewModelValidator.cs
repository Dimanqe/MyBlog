using FluentValidation;
using MyBlog.BLL.ViewModels.Comments.Request;

namespace MyBlog.BLL.Validators.Users;

public class CreateCommentViewModelValidator : AbstractValidator<CreateCommentViewModel>
{
    public CreateCommentViewModelValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("Поле Содержание обязательно для заполнения!");
        RuleFor(x => x.Author).NotEmpty().WithMessage("Поле Автор обязательно для заполнения!");
    }
}