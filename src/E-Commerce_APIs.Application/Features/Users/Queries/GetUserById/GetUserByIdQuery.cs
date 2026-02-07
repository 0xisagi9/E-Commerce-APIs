using E_Commerce_APIs.Application.DTOs;
using MediatR;
using E_Commerce_APIs.Shared.Helpers;

namespace E_Commerce_APIs.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<Result<UserDto>>
{
    public Guid Id { get; set; }
}
