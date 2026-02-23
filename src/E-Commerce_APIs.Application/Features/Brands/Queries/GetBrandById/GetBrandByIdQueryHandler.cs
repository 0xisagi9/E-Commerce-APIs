using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Services;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Brands.Queries.GetBrandById;

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<BrandDTO>>
{
    private readonly BrandQueryService _brandQueryService;

    public GetBrandByIdQueryHandler(BrandQueryService brandQueryService)
    {
        _brandQueryService = brandQueryService;
    }

    public async Task<Result<BrandDTO>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        // Get brand by ID
        var brandDto = await _brandQueryService.GetBrandByIdAsync(request.Id);

        if (brandDto == null)
            return Result<BrandDTO>.NotFound($"Brand with Id:{request.Id} is not found", 204);

        return Result<BrandDTO>.Success(brandDto);
    }
}
