using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;
using AutoMapper;

namespace E_Commerce_APIs.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result<BrandDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<BrandDTO>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        // 1) Check if brand exists
        var brand = await _unitOfWork.Brands.GetByIdAsync(request.Id);
        if (brand == null)
            return Result<BrandDTO>.Failure("Brand not found", 400);

        // 2) Update fields if provided
        if (!string.IsNullOrWhiteSpace(request.Name))
            brand.Name = request.Name;

        // 3) Update modified date and save
        brand.ModifiedDate = DateTime.UtcNow;
        await _unitOfWork.Brands.UpdateAsync(brand);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 4) Return updated brand
        var brandDto = _mapper.Map<BrandDTO>(brand);

        return Result<BrandDTO>.Success(brandDto, "Brand updated successfully");
    }
}
