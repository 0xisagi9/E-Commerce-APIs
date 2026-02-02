namespace E_Commerce_APIs.Application.Common.Interfaces;

using E_Commerce_APIs.Domain.Entities;

/// <summary>
/// Handles role extraction and role-related operations
/// Centralizes role-specific logic
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Gets all role names for a user
    /// </summary>
    IEnumerable<string> GetUserRoleNames(User user);

    /// <summary>
    /// Gets the primary role name for a user (first role)
    /// </summary>
    string? GetUserPrimaryRoleName(User user);
}
