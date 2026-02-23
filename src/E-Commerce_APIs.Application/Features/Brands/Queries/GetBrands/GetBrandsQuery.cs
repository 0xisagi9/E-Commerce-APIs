using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Brands.Queries.GetBrands;

public class GetBrandsQuery : IRequest<PaginatedResult<BrandDTO>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public bool? isDeleted { get; set; } = false;
}
