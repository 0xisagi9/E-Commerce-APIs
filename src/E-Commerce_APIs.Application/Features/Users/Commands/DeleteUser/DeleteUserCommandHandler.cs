using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_APIs.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        //1) Check For User is Found Or Not
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id);
        if (user == null)
            return Result.NotFound("User Not Found", 204);

        //2) Update the Fields (Soft Delete)
        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.ModifiedDate = DateTime.UtcNow;

        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        //3) Return Result
        return Result.Success();
    }
}
