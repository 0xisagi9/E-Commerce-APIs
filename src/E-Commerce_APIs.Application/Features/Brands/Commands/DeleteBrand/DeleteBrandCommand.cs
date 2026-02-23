using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommand : IRequest<Result>
{
    public int Id { get; set; }
}
