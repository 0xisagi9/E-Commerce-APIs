namespace E_Commerce_APIs.Application.Common.Interfaces;

using E_Commerce_APIs.Domain.Entities;
using System.Linq.Expressions;

/// <summary>
/// Handles user query and retrieval operations
/// Centralizes data access logic for user queries
/// </summary>
public interface IUserQueryService
{
    /// <summary>
    /// Gets users with optional filtering
    /// </summary>
    Task<(List<User> users, int totalCount)> GetUsersAsync(
        Expression<Func<User, bool>> specification,
        int skip,
        int take,
        string? sortBy,
        string? sortOrder);
}
