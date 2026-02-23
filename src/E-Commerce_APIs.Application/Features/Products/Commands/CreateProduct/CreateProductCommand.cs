using MediatR;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;

namespace E_Commerce_APIs.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Result<ProductDTO>>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? SmallImage { get; set; }
    public int? BrandId { get; set; }
    public string? Feature { get; set; }
    public List<int> CategoryIds { get; set; } = new List<int>();
}
