using E_Commerce_APIs.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;
using E_Commerce_APIs.Shared.Helpers;
namespace E_Commerce_APIs.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<Result<AuthResponseDto>>
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}
