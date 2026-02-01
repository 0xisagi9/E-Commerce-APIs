using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using FluentValidation;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Users.Commands.RegisterUser;
public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(x => x.UserName)
          .NotEmpty().WithMessage("Username is required")
          .MinimumLength(3).WithMessage("Username must be at least 3 characters")
          .MaximumLength(50).WithMessage("Username cannot exceed 50 characters")
          .Matches("^[a-zA-Z0-9_-]+$").WithMessage("Username can only contain letters, numbers, underscores, and hyphens")
          .MustAsync(BeUniqueUserName).WithMessage("Username is already taken");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters")
            .MustAsync(BeUniqueEmail).WithMessage("Email is already registered");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
            .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

        RuleFor(x => x.ConfirmPassword)
        .NotEmpty().WithMessage("Password confirmation is required")
        .Equal(x => x.Password).WithMessage("Passwords do not match");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(100).WithMessage("First name cannot exceed 100 characters")
            .Matches("^[a-zA-Z ]+$").WithMessage("First name can only contain letters and spaces");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters")
            .Matches("^[a-zA-Z ]+$").WithMessage("Last name can only contain letters and spaces");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters")
            .Matches(@"^(0020|\+20|0)?1[0125][0-9]{8}$").WithMessage("Invalid phone number format")
            .MustAsync(BeUniquePhoneNumber).WithMessage("Phone Number already Exist");
    }

    private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByUserNameAsync(userName);
        return user == null;
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);
        return user == null;
    }

    private async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellationToken)
    {
        if (phoneNumber is not null)
        {
            var phoneNumberExists = await _unitOfWork.Users.GetByPhoneNumberAsync(phoneNumber);
            return phoneNumberExists == null;
        }
        return true;
    }
}


