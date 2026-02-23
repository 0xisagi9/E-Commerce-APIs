using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using System.Linq.Expressions;

namespace E_Commerce_APIs.Application.Features.Brands.Specification;

public class BrandFilterSpecification : ISpecification<Brand>
{
    private readonly bool? _isDeleted;

    public BrandFilterSpecification(bool? isDeleted)
    {
        _isDeleted = isDeleted;
    }

    public Expression<Func<Brand, bool>> Criteria => BuildCriteria()!;

    public Expression<Func<Brand, bool>>? BuildCriteria()
    {
        Expression<Func<Brand, bool>> predicate = b => true;
        if (_isDeleted.HasValue)
            predicate = CombineAnd(predicate, b => b.IsDeleted == _isDeleted.Value);

        return predicate;
    }

    private static Expression<Func<Brand, bool>> CombineAnd(Expression<Func<Brand, bool>> left, Expression<Func<Brand, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(Brand));
        var combined = Expression.AndAlso(
            Expression.Invoke(left, parameter),
            Expression.Invoke(right, parameter)
        );
        return Expression.Lambda<Func<Brand, bool>>(combined, parameter);
    }
}
