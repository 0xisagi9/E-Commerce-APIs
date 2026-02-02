using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Application.Services;

/// <summary>
/// Handles role extraction and role-related operations
/// Centralizes role-specific business logic
/// </summary>
public class RoleService : IRoleService
{
    public IEnumerable<string> GetUserRoleNames(User user)
    {
        if (user?.UserRoles == null || !user.UserRoles.Any())
            return Enumerable.Empty<string>();

        return user.UserRoles
            .Where(ur => ur.Role != null)
            .Select(ur => ur.Role.Name)
            .ToList();
    }

    public string? GetUserPrimaryRoleName(User user)
    {
        return GetUserRoleNames(user).FirstOrDefault();
    }
}
