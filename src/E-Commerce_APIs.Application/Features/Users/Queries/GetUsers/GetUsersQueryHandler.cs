using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Features.Users.Specifications;
using E_Commerce_APIs.Application.Services;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using System.Linq.Expressions;
using E_Commerce_APIs.Domain.Entities;
using AutoMapper;

namespace E_Commerce_APIs.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResult<UserDto>>
    {
        private readonly UserQueryService _userQueryService;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(UserQueryService userQueryService, IMapper mapper)
        {
            _userQueryService = userQueryService;
            _mapper = mapper;
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

            // Get users with pagination and sorting (includes roles automatically)
            var (users, totalCount) = await _userQueryService.GetUsersWithRolesAsync(
                specification.Criteria,
                skip,
                request.PageSize,
                request.SortBy,
                request.SortOrder);

            // Map to DTOs using AutoMapper and enrich with roles
            var userDtos = users.Select(u =>
            {
                var userDto = _mapper.Map<UserDto>(u);
                userDto.Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList();
                return userDto;
            }).ToList();

            // Build paginated result
            return _userQueryService.CreatePaginatedResult(
                userDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);
        }
    }
}
