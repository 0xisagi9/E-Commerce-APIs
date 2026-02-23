using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRequest<Result<BrandDTO>>
{
    public string Name { get; set; } = string.Empty;
}
