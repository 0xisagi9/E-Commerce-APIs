using E_Commerce_APIs.Application.Features.Brands.Commands.CreateBrand;
using FluentValidation;

namespace E_Commerce_APIs.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Brand name is required")
            .MaximumLength(255).WithMessage("Brand name must not exceed 255 characters");
    }
}
