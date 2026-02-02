using MediatR;
using FluentValidation;
using E_Commerce_APIs.Application.Exceptions;
using FluentValidation.Results;

namespace E_Commerce_APIs.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        // If no validators are registered, continue
        if (!_validators.Any())
            return await next();

        // Create validation context
        var context = new ValidationContext<TRequest>(request);

        // Run validators sequentially to avoid concurrent DbContext access
        var validationResults = new List<FluentValidation.Results.ValidationResult>();
        foreach (var validator in _validators)
        {
            var result = await validator.ValidateAsync(context, cancellationToken);
            validationResults.Add(result);
        }

        // Collect all failures
        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToList();

        // If there are validation errors, throw exception
        if (failures.Any())
        {
            throw new Exceptions.ValidationException(failures);
        }

        // Continue to next behavior or handler
        return await next();

    }
}
