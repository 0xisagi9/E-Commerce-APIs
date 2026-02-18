using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;
using E_Commerce_APIs.Domain.Entities;


namespace E_Commerce_APIs.Application.Features.Vendors.Commands.CreateVendor;

public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, Result<VendorDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateVendorCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<VendorDTO>> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var vendor = new Vendor()
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                WebsiteUrl = request.WebsiteUrl
            };

            vendor = await _unitOfWork.Vendors.AddAsync(vendor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();

            var vendorDTO = new VendorDTO()
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Email = vendor.Email,
                PhoneNumber = vendor.PhoneNumber,
                AverageRate = vendor.AverageRate,
                Slug = vendor.Slug,
                WebsiteUrl = vendor.WebsiteUrl,
            };

            return Result<VendorDTO>.Success(vendorDTO, "Vendor created successfully", 201);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            // ✅ FIXED: Re-throw the actual exception for debugging
            throw;
        }
    }
}
