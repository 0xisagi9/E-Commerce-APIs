using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_APIs.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}
