using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Application.Services;

public class VendorSortService : GenericSortServiceBase<Vendor>
{
    protected override string[] ValidSortFields => new[]
    {
        "name", "email", "created_date", "modified_date", "average_rate"
    };

    protected override string DefaultSortField => "created_date";

    public override IQueryable<Vendor> ApplySorting(IQueryable<Vendor> query, string? sortBy, string? sortOrder)
    {
        var (normalizedSortBy, isDescending) = GetSortOptions(sortBy, sortOrder);

        return normalizedSortBy switch
        {
            "name" => ApplyOrdering(query, normalizedSortBy, isDescending, v => v.Name),
            "email" => ApplyOrdering(query, normalizedSortBy, isDescending, v => v.Email),
            "created_date" => ApplyOrdering(query, normalizedSortBy, isDescending, v => v.CreationDate),
            "modified_date" => ApplyOrdering(query, normalizedSortBy, isDescending, v => v.ModifiedDate),
            "average_rate" => ApplyOrdering(query, normalizedSortBy, isDescending, v => v.AverageRate ?? 0),
            _ => ApplyOrdering(query, normalizedSortBy, isDescending, v => v.CreationDate)
        };
    }
}
