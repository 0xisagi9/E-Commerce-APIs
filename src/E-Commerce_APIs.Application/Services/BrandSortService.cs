using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Application.Services;

public class BrandSortService : GenericSortServiceBase<Brand>
{
    protected override string[] ValidSortFields => new[]
    {
        "name", "createdate", "modifieddate"
    };

    protected override string DefaultSortField => "createdate";

    public override IQueryable<Brand> ApplySorting(IQueryable<Brand> query, string? sortBy, string? sortOrder)
    {
        var (normalizedSortBy, isDescending) = GetSortOptions(sortBy, sortOrder);

        return normalizedSortBy switch
        {
            "name" => ApplyOrdering(query, normalizedSortBy, isDescending, b => b.Name),
            "createdate" => ApplyOrdering(query, normalizedSortBy, isDescending, b => b.CreationDate),
            "modifieddate" => ApplyOrdering(query, normalizedSortBy, isDescending, b => b.ModifiedDate),
            _ => ApplyOrdering(query, normalizedSortBy, isDescending, b => b.CreationDate)
        };
    }
}
