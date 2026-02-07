using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Features.Users.Specifications;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResult<UserDto>>
    {
        private readonly IUserQueryService _userQueryService;
        private readonly IUserDtoMapper _userDtoMapper;

        public GetUsersQueryHandler(IUserQueryService userQueryService, IUserDtoMapper userDtoMapper)
        {
            _userQueryService = userQueryService;
            _userDtoMapper = userDtoMapper;
        }

        public async Task<PaginatedResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            // Create filter specification
            var specification = new UserFilterSpecification(
                request.IsVerified,
                request.IsDeleted,
                request.RoleId);

            // Calculate pagination
            var skip = (request.PageNumber - 1) * request.PageSize;

            // Get users with pagination and sorting
            var (users, totalCount) = await _userQueryService.GetUsersAsync(
                specification.Criteria,
                skip,
                request.PageSize,
                request.SortBy,
                request.SortOrder);

            // Map to DTOs
            var userDtos = _userDtoMapper.MapToUserDtos(users);

            // Build paginated result
            var result = _userDtoMapper.CreatePaginatedResult(
                userDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return result;
        }
    }
}
