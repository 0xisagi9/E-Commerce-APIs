using E_Commerce_APIs.Application.Common.Interfaces;

namespace E_Commerce_APIs.Application.Services;

/// <summary>
/// Handles sorting logic for user queries
/// Centralizes and normalizes sort options
/// </summary>
public class UserSortService : IUserSortService
{
    /// <summary>
    /// Normalizes and validates sort parameters
    /// Returns normalized sort field and direction
    /// </summary>
    public (string sortBy, bool isDescending) GetSortOptions(string? sortBy, string? sortOrder)
    {
        var normalizedSortBy = (sortBy ?? "created_at").ToLower();
        var isDescending = (sortOrder ?? "desc").ToLower() == "desc";

        // Validate sort field
        var validSortFields = new[] { "username", "email", "firstname", "lastname", "created_at", "modified_date" };
        if (!validSortFields.Contains(normalizedSortBy))
        {
            normalizedSortBy = "created_at";
        }

        return (normalizedSortBy, isDescending);
    }
}
