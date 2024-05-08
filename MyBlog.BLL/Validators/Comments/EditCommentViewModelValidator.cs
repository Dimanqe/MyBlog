using FluentValidation;
using MyBlog.BLL.ViewModels.Comments.Request;

namespace MyBlog.BLL.Validators.Users;

public class EditCommentViewModelValidator : AbstractValidator<EditCommentViewModel>
{
    public EditCommentViewModelValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("Поле Содержание обязательно для заполнения!");
    }
}