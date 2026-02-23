using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_APIs.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Result<UserDto>>
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
}
