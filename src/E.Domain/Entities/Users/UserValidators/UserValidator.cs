﻿using FluentValidation;

namespace E.Domain.Entities.Users.UserValidators;

public class UserValidator : AbstractValidator<DomainUser>
{
    public UserValidator()
    {
        RuleFor(u => u.FullName)
            .NotNull().WithMessage("Fullname is required. It is currently null")
            .MinimumLength(3).WithMessage("Fullname must be at least 3 characters long")
            .MaximumLength(60).WithMessage("Fullname can contain at most 50 characters long");

        RuleFor(u => u.Email)
            .NotNull().WithMessage("Email is required. It is currently null")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(u => u.CreatedDate)
            .NotNull().WithMessage("CreatedDate is required. It is currently null")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedDate cannot be in the future");

        RuleFor(u => u.Avatar)
            .Must(avatar => string.IsNullOrEmpty(avatar) || Uri.IsWellFormedUriString(avatar, UriKind.Absolute))
            .WithMessage("Invalid URL for Avatar");

        RuleFor(u => u.Address)
            .MaximumLength(100).WithMessage("Address can contain at most 100 characters long")
            .When(u => !string.IsNullOrEmpty(u.Address));

        RuleFor(u => u.CurrentCity)
            .MaximumLength(50).WithMessage("CurrentCity can contain at most 50 characters long")
            .When(u => !string.IsNullOrEmpty(u.CurrentCity));
    }
}
