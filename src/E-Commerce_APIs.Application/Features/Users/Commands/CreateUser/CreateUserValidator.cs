using FluentValidation;

namespace E_Commerce_APIs.Application.Features.Users.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(p => p.UserName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20)
            .WithMessage("Username must not exceed 20 characters.");

        RuleFor(p => p.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .MaximumLength(255)
            .WithMessage("A valid email is required.");

        RuleFor(p => p.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .MaximumLength(255)
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Password must contain at least one special character (!? *.).");

        RuleFor(p => p.FirstName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50)
            .WithMessage("First name is required and must not exceed 50 characters.");

        RuleFor(p => p.LastName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50)
            .WithMessage("Last name is required and must not exceed 50 characters.");

        RuleFor(p => p.PhoneNumber)
            .MaximumLength(20)
            .Matches(@"^\+?[1-9]\d{1,19}$")
            .WithMessage("Phone number must be valid and not exceed 20 characters.");
    }

}
