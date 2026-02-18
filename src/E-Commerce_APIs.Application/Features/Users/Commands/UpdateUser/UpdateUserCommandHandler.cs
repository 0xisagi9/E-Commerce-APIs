using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_APIs.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
{
    private readonly IUnitOfWork _unitofWork;
    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitofWork = unitOfWork;
    }
    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        //1) Check for Existance of User
        var user = await _unitofWork.Users.GetByIdAsync(request.Id);
        if (user is null)
            return Result<UserDto>.Failure("User not Found", 400);
        //2) Update Faileds and Assign it to user
        if (!string.IsNullOrWhiteSpace(request.UserName))
            user.UserName = request.UserName;
        if (!string.IsNullOrWhiteSpace(request.Email))
            user.Email = request.Email;
        if (!string.IsNullOrWhiteSpace(request.FirstName))
            user.FirstName = request.FirstName;
        if (!string.IsNullOrWhiteSpace(request.LastName))
            user.LastName = request.LastName;
        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            user.PhoneNumber = request.PhoneNumber;
        //3) Call User Update
        user.ModifiedDate = DateTime.UtcNow;
        await _unitofWork.Users.UpdateAsync(user);
        await _unitofWork.SaveChangesAsync();
        //4) Return Updated User
        var response = new UserDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsVerified = user.IsVerified,
        };

        return Result<UserDto>.Success(response);
    }
}
