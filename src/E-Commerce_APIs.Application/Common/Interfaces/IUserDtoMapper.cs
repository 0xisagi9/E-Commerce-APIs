namespace E_Commerce_APIs.Application.Common.Interfaces;

using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Helpers;

/// <summary>
/// Handles user data mapping and transformation
/// Converts domain models to DTOs with enriched data
/// </summary>
public interface IUserDtoMapper
{
    /// <summary>
    /// Maps a collection of User entities to UserDto collection
    /// Includes role information
    /// </summary>
    List<UserDto> MapToUserDtos(List<User> users);

    /// <summary>
    /// Creates a paginated result from users and metadata
    /// </summary>
    PaginatedResult<UserDto> CreatePaginatedResult(
        List<UserDto> userDtos,
        int pageNumber,
        int pageSize,
        int totalCount);
}
