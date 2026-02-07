namespace E_Commerce_APIs.Application.Common.Interfaces;

/// <summary>
/// Handles sorting logic for user queries
/// Centralizes sorting behavior
/// </summary>
public interface IUserSortService
{
    /// <summary>
    /// Gets the sort key and direction for a user query
    /// </summary>
    (string sortBy, bool isDescending) GetSortOptions(string? sortBy, string? sortOrder);
}
