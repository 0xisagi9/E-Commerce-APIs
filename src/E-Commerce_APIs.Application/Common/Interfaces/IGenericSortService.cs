namespace E_Commerce_APIs.Application.Common.Interfaces;

using System.Linq.Expressions;

/// <summary>
/// Generic interface for sorting any entity
/// Centralizes sorting logic reusable across all queries
/// </summary>
public interface IGenericSortService<TEntity> where TEntity : class
{
    /// <summary>
    /// Gets sort options (normalized field name and direction)
    /// </summary>
    (string sortBy, bool isDescending) GetSortOptions(string? sortBy, string? sortOrder);

    /// <summary>
    /// Applies sorting to a queryable collection
    /// </summary>
    IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, string? sortBy, string? sortOrder);
}
