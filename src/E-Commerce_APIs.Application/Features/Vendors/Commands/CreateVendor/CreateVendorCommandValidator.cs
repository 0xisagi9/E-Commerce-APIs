using E_Commerce_APIs.Shared.Interfaces;
using FluentValidation;

namespace E_Commerce_APIs.Application.Features.Vendors.Commands.CreateVendor;

public class CreateVendorCommandValidator : AbstractValidator<CreateVendorCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateVendorCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Vendor name is required.")
            .MaximumLength(255).WithMessage("Vendor name cannot exceed 255 characters.")
            .MustAsync(BeUniqueName).WithMessage("Name is Already Token");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.")
            .EmailAddress().WithMessage("A valid email address must be provided.")
            .MustAsync(BeUniqueEmail).WithMessage("Email is already registered");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.")
            .Matches(@"^\+?[\d\s\-\(\)]+$").WithMessage("Phone number must contain only digits, spaces, hyphens, and parentheses.")
            .MustAsync(BeUniquePhoneNumber).WithMessage("Phone Number already Exist");

        RuleFor(x => x.WebsiteUrl)
            .MaximumLength(500).WithMessage("Website URL cannot exceed 500 characters.")
            .Must(BeAValidUrl).WithMessage("Website URL must be a valid URL (e.g., http://example.com).")
            .When(x => !string.IsNullOrEmpty(x.WebsiteUrl));
    }
    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        var vendor = await _unitOfWork.Vendors.GetByNameAsync(name);
        return vendor == null;
    }
    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        var vendor = await _unitOfWork.Vendors.GetByEmailAsync(email);
        return vendor == null;
    }

    private async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellationToken)
    {
        var vendor = await _unitOfWork.Vendors.GetByPhoneNumber(phoneNumber);
        return vendor == null;
    }

}
