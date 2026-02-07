using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce_APIs.Application.Services;

/// <summary>
/// Handles user query and retrieval operations
/// Centralizes data access logic with sorting and pagination
/// </summary>
public class UserQueryService : IUserQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSortService _sortService;

    public UserQueryService(IUnitOfWork unitOfWork, IUserSortService sortService)
    {
        _unitOfWork = unitOfWork;
        _sortService = sortService;
    }

    public async Task<(List<User> users, int totalCount)> GetUsersAsync(
        Expression<Func<User, bool>> specification,
        int skip,
        int take,
        string? sortBy,
        string? sortOrder)
    {
        var userRepository = _unitOfWork.Users;

        // Build query with specification
        var query = userRepository.GetQueryble()
            .Where(specification);

        // Get total count before pagination
        var totalCount = await query.CountAsync();

        // Apply sorting
        query = ApplySorting(query, sortBy, sortOrder);

        // Apply pagination and load related data
        var users = await query
            .Skip(skip)
            .Take(take)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();

        return (users, totalCount);
    }

    private IQueryable<User> ApplySorting(IQueryable<User> query, string? sortBy, string? sortOrder)
    {
        var (normalizedSortBy, isDescending) = _sortService.GetSortOptions(sortBy, sortOrder);

        return normalizedSortBy switch
        {
            "username" => isDescending ? query.OrderByDescending(u => u.UserName) : query.OrderBy(u => u.UserName),
            "email" => isDescending ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
            "firstname" => isDescending ? query.OrderByDescending(u => u.FirstName) : query.OrderBy(u => u.FirstName),
            "lastname" => isDescending ? query.OrderByDescending(u => u.LastName) : query.OrderBy(u => u.LastName),
            "created_at" => isDescending ? query.OrderByDescending(u => u.CreatedAt) : query.OrderBy(u => u.CreatedAt),
            "modified_date" => isDescending ? query.OrderByDescending(u => u.ModifiedDate) : query.OrderBy(u => u.ModifiedDate),
            _ => query.OrderByDescending(u => u.CreatedAt)
        };
    }
}
