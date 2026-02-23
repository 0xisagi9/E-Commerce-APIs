using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using Npgsql.Replication;
using System.Linq.Expressions;

namespace E_Commerce_APIs.Application.Features.Vendors.Specification;

public class VendorFilterSpecification : ISpecification<Vendor>
{
    private readonly bool? _isDeleted;

    public VendorFilterSpecification(bool? isDeleted)
    {
        _isDeleted = isDeleted;
    }
    public Expression<Func<Vendor, bool>> Criteria => BuildCriteria()!;



    public Expression<Func<Vendor, bool>>? BuildCriteria()
    {
        Expression<Func<Vendor, bool>> predicate = u => true;
        if (_isDeleted.HasValue)
            predicate = CombineAnd(predicate, v => v.IsDeleted == _isDeleted.Value);

        return predicate;
    }
    private static Expression<Func<Vendor, bool>> CombineAnd(Expression<Func<Vendor, bool>> left, Expression<Func<Vendor, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(Vendor));
        var combined = Expression.AndAlso(
            Expression.Invoke(left, parameter),
            Expression.Invoke(right, parameter)
        );
        return Expression.Lambda<Func<Vendor, bool>>(combined, parameter);
    }
}
