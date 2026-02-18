using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Vendors.Commands.UpdateVendor;

public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, Result<VendorDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVendorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<VendorDTO>> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        // 1) Check if vendor exists
        var vendor = await _unitOfWork.Vendors.GetByIdAsync(request.Id);
        if (vendor == null)
            return Result<VendorDTO>.Failure("Vendor not found", 400);

        // 2) Update fields if provided
        if (!string.IsNullOrWhiteSpace(request.Name))
            vendor.Name = request.Name;
        if (!string.IsNullOrWhiteSpace(request.Email))
            vendor.Email = request.Email;
        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            vendor.PhoneNumber = request.PhoneNumber;
        if (!string.IsNullOrWhiteSpace(request.WebsiteUrl))
            vendor.WebsiteUrl = request.WebsiteUrl;

        // 3) Update modified date and save
        vendor.ModifiedDate = DateTime.UtcNow;
        await _unitOfWork.Vendors.UpdateAsync(vendor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 4) Return updated vendor
        var vendorDto = new VendorDTO
        {
            Id = vendor.Id,
            Name = vendor.Name,
            Email = vendor.Email,
            PhoneNumber = vendor.PhoneNumber,
            WebsiteUrl = vendor.WebsiteUrl,
            AverageRate = vendor.AverageRate,
            Slug = vendor.Slug
        };

        return Result<VendorDTO>.Success(vendorDto, "Vendor updated successfully");
    }
}
