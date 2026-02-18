using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Features.Vendors.Specification;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Application.Services;
using MediatR;
using System.Linq.Expressions;
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Application.Features.Vendors.Queries.GetVendors;

public class GetVendorQueryHandler : IRequestHandler<GetVendorsQuery, PaginatedResult<VendorDTO>>
{
    private readonly VendorQueryService _vendorQueryService;

    public GetVendorQueryHandler(VendorQueryService vendorQueryService)
    {
        _vendorQueryService = vendorQueryService;
    }

    public async Task<PaginatedResult<VendorDTO>> Handle(GetVendorsQuery request, CancellationToken cancellationToken)
    {
        // Create filter specification
        var specification = new VendorFilterSpecification(request.isDeleted);

        // Calculate pagination
        var skip = (request.PageNumber - 1) * request.PageSize;

        // Get vendors with pagination and sorting
        var (vendors, totalCount) = await _vendorQueryService.GetEntitiesAsync(
            specification.Criteria,
            skip,
            request.PageSize,
            request.SortBy,
            request.SortOrder);

        // Map to DTOs using inline mapping
        var vendorDtos = vendors.Select(v => new VendorDTO
        {
            Id = v.Id,
            Name = v.Name,
            Email = v.Email,
            PhoneNumber = v.PhoneNumber,
            WebsiteUrl = v.WebsiteUrl,
            AverageRate = v.AverageRate,
            Slug = v.Slug
        }).ToList();

        // Build paginated result
        return _vendorQueryService.CreatePaginatedResult(
            vendorDtos,
            request.PageNumber,
            request.PageSize,
            totalCount);
    }
}
