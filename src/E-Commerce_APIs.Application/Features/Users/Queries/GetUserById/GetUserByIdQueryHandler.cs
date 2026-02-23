using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Services;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly UserQueryService _userQueryService;

    public GetUserByIdQueryHandler(UserQueryService userQueryService)
    {
        _userQueryService = userQueryService;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Get user by ID with roles
        var userDto = await _userQueryService.GetUserByIdAsync(request.Id);

        if (userDto == null)
            return Result<UserDto>.NotFound($"User with Id:{request.Id} is not found", 204);

        return Result<UserDto>.Success(userDto);
    }
}
