using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Domain.Entities;
using System.Linq.Expressions;
using E_Commerce_APIs.Shared.Interfaces;
namespace E_Commerce_APIs.Application.Features.Users.Specifications;

public class UserFilterSpecification : ISpecification<User>
{
    public Expression<Func<User, bool>> Criteria => BuildCriteria()!;
    private readonly bool? _isVerified;
    private readonly bool? _isDeleted;
    private readonly int? _roleId;

    public UserFilterSpecification(bool? isVerified, bool? isDeleted, int? roleId)
    {
        _isVerified = isVerified;
        _isDeleted = isDeleted;
        _roleId = roleId;
    }

    private Expression<Func<User, bool>>? BuildCriteria()
    {
        Expression<Func<User, bool>> predicate = u => true;
        if (_isVerified.HasValue)
            predicate = CombineAnd(predicate, u => u.IsVerified == _isVerified.Value);
        if (_isDeleted.HasValue)
            predicate = CombineAnd(predicate, u => u.IsDeleted == _isDeleted.Value);
        if (_roleId.HasValue)
            predicate = CombineAnd(predicate, u => u.UserRoles.Any(ur => ur.RoleId == _roleId.Value));

        return predicate;

    }
    private Expression<Func<User, bool>> CombineAnd(Expression<Func<User, bool>> left, Expression<Func<User, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(User));
        var combined = Expression.AndAlso(
            Expression.Invoke(left, parameter),
            Expression.Invoke(right, parameter)
        );
        return Expression.Lambda<Func<User, bool>>(combined, parameter);
    }
}
