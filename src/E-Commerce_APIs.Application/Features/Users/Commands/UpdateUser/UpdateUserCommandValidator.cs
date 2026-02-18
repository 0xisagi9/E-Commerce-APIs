using E_Commerce_APIs.Shared.Interfaces;
using FluentValidation;


namespace E_Commerce_APIs.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;


        RuleFor(x => x.UserName)
            .MustAsync(BeUniqueUserName).WithMessage("Username is already taken")
            .When(x => !string.IsNullOrWhiteSpace(x.UserName))
            .DependentRules(() =>
            {
                RuleFor(x => x.UserName)
                    .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                    .MaximumLength(50).WithMessage("Username cannot exceed 50 characters")
                    .Matches("^[a-zA-Z0-9_-]+$").WithMessage("Username can only contain letters, numbers, underscores, and hyphens");
            });

        RuleFor(x => x.Email)
            .MustAsync(BeUniqueEmail).WithMessage("Email is already registered")
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .DependentRules(() =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress().WithMessage("Invalid email format")
                    .MaximumLength(255).WithMessage("Email cannot exceed 255 characters");
            });

        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
            .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character")
            .When(x => !string.IsNullOrWhiteSpace(x.Password));

        RuleFor(x => x.FirstName)
            .MaximumLength(100).WithMessage("First name cannot exceed 100 characters")
            .Matches("^[a-zA-Z ]+$").WithMessage("First name can only contain letters and spaces")
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters")
            .Matches("^[a-zA-Z ]+$").WithMessage("Last name can only contain letters and spaces")
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.PhoneNumber)
            .MustAsync(BeUniquePhoneNumber).WithMessage("Phone Number already exists")
            .Matches(@"^(0020|\+20|0)?1[0125][0-9]{8}$").WithMessage("Invalid phone number format")
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

    }

    private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.Users.GetByUserNameAsync(userName);

        return existingUser == null;
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.Users.GetByEmailAsync(email);

        return existingUser == null;
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