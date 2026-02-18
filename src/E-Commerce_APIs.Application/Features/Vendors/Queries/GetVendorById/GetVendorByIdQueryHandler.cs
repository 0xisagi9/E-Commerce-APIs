using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Services;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Vendors.Queries.GetVendorById;

public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, Result<VendorDTO>>
{
    private readonly VendorQueryService _vendorQueryService;

    public GetVendorByIdQueryHandler(VendorQueryService vendorQueryService)
    {
        _vendorQueryService = vendorQueryService;
    }

    public async Task<Result<VendorDTO>> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        // Get vendor by ID with offers
        var vendorDto = await _vendorQueryService.GetVendorByIdAsync(request.Id);

        if (vendorDto == null)
            return Result<VendorDTO>.NotFound($"Vendor with Id:{request.Id} is not found", 204);

        return Result<VendorDTO>.Success(vendorDto);
    }
}
