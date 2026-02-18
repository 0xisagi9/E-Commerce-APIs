using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Vendors.Commands.DeleteVendor;

public class DeleteVendorCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}
