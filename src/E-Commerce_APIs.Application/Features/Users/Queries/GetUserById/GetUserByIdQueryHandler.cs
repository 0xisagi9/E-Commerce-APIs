using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserQueryService _userQueryService;

    public GetUserByIdQueryHandler(IUserQueryService userQueryService)
    {
        _userQueryService = userQueryService;
    }
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _userQueryService.GetUserByIdAsync(request.Id);

        if (response == null)
            return Result<UserDto>.NotFound($"User with Id:{request.Id} is not found", 204);

        return Result<UserDto>.Success(response);

    }
}
