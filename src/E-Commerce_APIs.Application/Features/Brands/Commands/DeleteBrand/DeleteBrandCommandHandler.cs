using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBrandCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        // 1) Check if brand exists
        var brand = await _unitOfWork.Brands.GetByIdAsync(request.Id);
        if (brand == null)
            return Result.NotFound("Brand not found", 204);

        // 2) Soft delete the brand
        brand.IsDeleted = true;
        brand.DeletedAt = DateTime.UtcNow;
        brand.ModifiedDate = DateTime.UtcNow;

        await _unitOfWork.Brands.UpdateAsync(brand);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 3) Return result
        return Result.Success("Brand deleted successfully");
    }
}
