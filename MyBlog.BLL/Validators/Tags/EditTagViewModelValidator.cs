﻿using FluentValidation;
using MyBlog.BLL.ViewModels.Tags.Request;

namespace MyBlog.BLL.Validators.Tags;

public class EditTagViewModelValidator : AbstractValidator<EditTagViewModel>
{
    public EditTagViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Поле Название обязательно для заполнения!");
    }
}