using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Brands.Queries.GetBrandById;

public class GetBrandByIdQuery : IRequest<Result<BrandDTO>>
{
    public int Id { get; set; }
}
