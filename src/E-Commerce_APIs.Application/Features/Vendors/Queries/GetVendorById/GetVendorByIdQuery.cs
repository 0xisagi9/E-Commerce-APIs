using E_Commerce_APIs.Application.DTOs;
using MediatR;
using E_Commerce_APIs.Shared.Helpers;

namespace E_Commerce_APIs.Application.Features.Vendors.Queries.GetVendorById;

public class GetVendorByIdQuery : IRequest<Result<VendorDTO>>
{
    public Guid Id { get; set; }
}
