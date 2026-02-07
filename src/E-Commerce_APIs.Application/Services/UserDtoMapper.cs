using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Helpers;

namespace E_Commerce_APIs.Application.Services;

/// <summary>
/// Handles user data mapping and transformation
/// Converts domain models to DTOs with enriched data
/// </summary>
public class UserDtoMapper : IUserDtoMapper
{
    private readonly IRoleService _roleService;

    public UserDtoMapper(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Maps a collection of User entities to UserDto collection
    /// Includes role information using RoleService
    /// </summary>
    public List<UserDto> MapToUserDtos(List<User> users)
    {
        return users.Select(u => MapToUserDto(u)).ToList();
    }

    /// <summary>
    /// Maps a single User entity to UserDto
    /// </summary>
    private UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            IsVerified = user.IsVerified,
            Roles = _roleService.GetUserRoleNames(user).ToList()
        };
    }

    /// <summary>
    /// Creates a paginated result from users and metadata
    /// </summary>
    public PaginatedResult<UserDto> CreatePaginatedResult(
        List<UserDto> userDtos,
        int pageNumber,
        int pageSize,
        int totalCount)
    {
        return new PaginatedResult<UserDto>
        {
            Items = userDtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalCount == 0 ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
