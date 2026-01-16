using MediatR;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce_APIs.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<Guid>
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
