using E_Commerce_APIs.Application.DTOs;
using MediatR;
using E_Commerce_APIs.Shared.Helpers;

namespace E_Commerce_APIs.Application.Features.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<Result<AuthResponseDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
