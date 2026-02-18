using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using System.Linq.Expressions;
namespace E_Commerce_APIs.Application.Common.Interfaces;


/// <summary>
/// Generic query service for retrieving and pagination any entity
/// Eliminates code duplication across all query handlers
/// </summary>
/// <typeparam name="TEntity">The entity type to query</typeparam>
/// <typeparam name="TDto">The DTO type to map to</typeparam>
public interface IGenericQueryService<TEntity, TDto> where TEntity : class where TDto : class
{
    Task<(List<TEntity> items, int totalCount)> GetEntitiesAsync(
        Expression<Func<TEntity, bool>>? specification,
        int skip, int take, string? sortBy, string? sortOrder,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null);

    Task<TEntity?> GetByIdAsync(
        object id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null);

    PaginatedResult<TDto> CreatePaginatedResult(
        List<TDto> items,
        int pageNumber,
        int pageSize,
        int totalCount);
}
