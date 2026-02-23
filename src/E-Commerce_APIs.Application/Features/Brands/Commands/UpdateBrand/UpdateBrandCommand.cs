using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommand : IRequest<Result<BrandDTO>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
