using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Features.Vendors.Specification;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Application.Services;
using MediatR;
using System.Linq.Expressions;
using E_Commerce_APIs.Domain.Entities;
using AutoMapper;

namespace E_Commerce_APIs.Application.Features.Vendors.Queries.GetVendors;

public class GetVendorQueryHandler : IRequestHandler<GetVendorsQuery, PaginatedResult<VendorDTO>>
{
    private readonly VendorQueryService _vendorQueryService;
    private readonly IMapper _mapper;

    public GetVendorQueryHandler(VendorQueryService vendorQueryService, IMapper mapper)
    {
        _vendorQueryService = vendorQueryService;
        _mapper = mapper;
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

        // Map to DTOs using AutoMapper
        var vendorDtos = _mapper.Map<List<VendorDTO>>(vendors);

        // Build paginated result
        return _vendorQueryService.CreatePaginatedResult(
            vendorDtos,
            request.PageNumber,
            request.PageSize,
            totalCount);
    }
}
