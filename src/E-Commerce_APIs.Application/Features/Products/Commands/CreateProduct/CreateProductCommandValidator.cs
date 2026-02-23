using FluentValidation;
using System.Text.Json;

namespace E_Commerce_APIs.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(255).WithMessage("Product name must not exceed 255 characters");

        RuleFor(p => p.Description)
            .MaximumLength(5000).WithMessage("Description must not exceed 5000 characters");

        RuleFor(p => p.SmallImage)
            .MaximumLength(500).WithMessage("Small image URL must not exceed 500 characters");

        // Validate Feature as JSON STRING
        RuleFor(p => p.Feature)
            .Custom((feature, context) =>
            {
                if (!string.IsNullOrWhiteSpace(feature))
                {
                    try
                    {
                        // Validate JSON structure
                        JsonDocument.Parse(feature);
                    }
                    catch (JsonException ex)
                    {
                        context.AddFailure("Feature", 
                            $"Feature must be valid JSON format. Error: {ex.Message}");
                    }
                }
            });

        RuleFor(p => p.CategoryIds)
            .NotNull().WithMessage("Category IDs collection is required");
    }
}
