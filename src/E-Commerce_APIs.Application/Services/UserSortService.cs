using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Application.Services;

public class UserSortService : GenericSortServiceBase<User>, IUserSortService
{
    protected override string[] ValidSortFields => new[]
        { "username", "email", "firstname", "lastname", "created_at", "modified_date" };

    protected override string DefaultSortField => "created_at";

    public override IQueryable<User> ApplySorting(IQueryable<User> query, string? sortBy, string? sortOrder)
    {
        var (normalizedSortBy, isDescending) = GetSortOptions(sortBy, sortOrder);

        return normalizedSortBy switch
        {
            "username" => ApplyOrdering(query, normalizedSortBy, isDescending, u => u.UserName),
            "email" => ApplyOrdering(query, normalizedSortBy, isDescending, u => u.Email),
            "firstname" => ApplyOrdering(query, normalizedSortBy, isDescending, u => u.FirstName),
            "lastname" => ApplyOrdering(query, normalizedSortBy, isDescending, u => u.LastName),
            "created_at" => ApplyOrdering(query, normalizedSortBy, isDescending, u => u.CreatedAt),
            "modified_date" => ApplyOrdering(query, normalizedSortBy, isDescending, u => u.ModifiedDate),
            _ => ApplyOrdering(query, normalizedSortBy, isDescending, u => u.CreatedAt)
        };
    }
}
