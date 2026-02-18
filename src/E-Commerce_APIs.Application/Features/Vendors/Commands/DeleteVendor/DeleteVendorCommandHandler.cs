using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Vendors.Commands.DeleteVendor;

public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVendorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {
        // 1) Check if vendor exists
        var vendor = await _unitOfWork.Vendors.GetByIdAsync(request.Id);
        if (vendor == null)
            return Result.NotFound("Vendor not found", 204);

        // 2) Soft delete the vendor
        vendor.IsDeleted = true;
        vendor.DeletedAt = DateTime.UtcNow;
        vendor.ModifiedDate = DateTime.UtcNow;

        await _unitOfWork.Vendors.UpdateAsync(vendor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 3) Return result
        return Result.Success("Vendor deleted successfully");
    }
}
