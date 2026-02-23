using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;
using E_Commerce_APIs.Domain.Entities;
using AutoMapper;


namespace E_Commerce_APIs.Application.Features.Vendors.Commands.CreateVendor;

public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, Result<VendorDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateVendorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

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

            var vendorDTO = _mapper.Map<VendorDTO>(vendor);

            return Result<VendorDTO>.Success(vendorDTO, "Vendor created successfully", 201);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
