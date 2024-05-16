using FluentValidation;
using MyBlog.BLL.ViewModels.Comments;

namespace MyBlog.BLL.Validators.Comments;

public class EditCommentViewModelValidator : AbstractValidator<EditCommentViewModel>
{
    public EditCommentViewModelValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("Поле Содержание обязательно для заполнения!");
    }
}