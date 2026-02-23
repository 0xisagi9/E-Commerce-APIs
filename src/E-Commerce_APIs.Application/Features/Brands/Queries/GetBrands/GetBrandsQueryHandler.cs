using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Features.Brands.Specification;
using E_Commerce_APIs.Application.Services;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using AutoMapper;

namespace E_Commerce_APIs.Application.Features.Brands.Queries.GetBrands;

public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, PaginatedResult<BrandDTO>>
{
    private readonly BrandQueryService _brandQueryService;
    private readonly IMapper _mapper;

    public GetBrandsQueryHandler(BrandQueryService brandQueryService, IMapper mapper)
    {
        _brandQueryService = brandQueryService;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<BrandDTO>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        // Create filter specification
        var specification = new BrandFilterSpecification(request.isDeleted);

        // Calculate pagination
        var skip = (request.PageNumber - 1) * request.PageSize;

        // Get brands with pagination and sorting
        var (brands, totalCount) = await _brandQueryService.GetEntitiesAsync(
            specification.Criteria,
            skip,
            request.PageSize,
            request.SortBy,
            request.SortOrder);

        // Map to DTOs using AutoMapper
        var brandDtos = _mapper.Map<List<BrandDTO>>(brands);

        // Build paginated result
        return _brandQueryService.CreatePaginatedResult(
            brandDtos,
            request.PageNumber,
            request.PageSize,
            totalCount);
    }
}
