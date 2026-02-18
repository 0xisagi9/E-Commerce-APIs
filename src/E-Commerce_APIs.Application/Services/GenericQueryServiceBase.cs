using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce_APIs.Application.Services;

public abstract class GenericQueryServiceBase<TEntity, TDto, TRepository> : IGenericQueryService<TEntity, TDto>
    where TEntity : class
    where TDto : class
    where TRepository : class, IBaseRepository<TEntity, Guid>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IGenericSortService<TEntity> _sortService;

    protected GenericQueryServiceBase(IUnitOfWork unitOfWork, IGenericSortService<TEntity> sortService)
    {
        _unitOfWork = unitOfWork;
        _sortService = sortService;
    }

    protected abstract TRepository GetRepository();
    protected abstract TDto MapToDto(TEntity entity);

    protected virtual List<TDto> MapToDtos(List<TEntity> entities) => entities.Select(MapToDto).ToList();

    public virtual async Task<(List<TEntity> items, int totalCount)> GetEntitiesAsync(
        Expression<Func<TEntity, bool>>? specification,
        int skip,
        int take,
        string? sortBy,
        string? sortOrder,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null)
    {
        var repository = GetRepository();
        var query = repository.GetQueryble();

        if (specification != null)
            query = query.Where(specification);

        var totalCount = await query.CountAsync();

        query = _sortService.ApplySorting(query, sortBy, sortOrder);

        if (includeFunc != null)
            query = includeFunc(query);

        var items = await query.Skip(skip).Take(take).ToListAsync();

        return (items, totalCount);
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        object id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null)
    {
        var repository = GetRepository();
        
        if (id is not Guid guidId)
            guidId = (Guid)Convert.ChangeType(id, typeof(Guid));

        if (includeFunc == null)
            return await repository.GetByIdAsync(guidId);

        var query = repository.GetQueryble();
        query = includeFunc(query);
        return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == guidId);
    }

    public virtual PaginatedResult<TDto> CreatePaginatedResult(List<TDto> items, int pageNumber, int pageSize, int totalCount)
    {
        return new PaginatedResult<TDto>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalCount == 0 ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
