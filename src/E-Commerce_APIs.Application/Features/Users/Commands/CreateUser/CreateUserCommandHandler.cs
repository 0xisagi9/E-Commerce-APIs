using E_Commerce_APIs.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using E_Commerce_APIs.Shared.Helpers;
using System.Data.SqlTypes;
using E_Commerce_APIs.Shared.Interfaces;
using E_Commerce_APIs.Application.Common.Interfaces;
using AutoMapper;
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRegistrationService _userRegistrationService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;


    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRegistrationService userRegistrationService, IPasswordHasher passwordHasher, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRegistrationService = userRegistrationService;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }
    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            //Create a User
            var user = await _userRegistrationService.CreateUserAsync(
                request.UserName,
                request.Email,
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                _passwordHasher.HashPassword(request.Password)
            );

            // Assign Role
            await _userRegistrationService.AssignRoleAsync(user, new[] { request.Role });

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            // return response using AutoMapper
            var result = _mapper.Map<UserDto>(user);
            result.Roles = new List<string> { request.Role };

            return Result<UserDto>.Success(result, $"Create User with Id:{user.Id} Successfully", 201);

        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
