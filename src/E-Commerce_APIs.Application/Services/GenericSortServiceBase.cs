using E_Commerce_APIs.Application.Common.Interfaces;
using System.Linq.Expressions;

namespace E_Commerce_APIs.Application.Services;

public abstract class GenericSortServiceBase<TEntity> : IGenericSortService<TEntity> where TEntity : class
{
    protected abstract string[] ValidSortFields { get; }
    protected abstract string DefaultSortField { get; }

    public (string sortBy, bool isDescending) GetSortOptions(string? sortBy, string? sortOrder)
    {
        var normalizedSortBy = (sortBy ?? DefaultSortField).ToLower();
        var isDescending = (sortOrder ?? "desc").ToLower() == "desc";

        if (!ValidSortFields.Contains(normalizedSortBy))
            normalizedSortBy = DefaultSortField;

        return (normalizedSortBy, isDescending);
    }

    public abstract IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, string? sortBy, string? sortOrder);

    protected IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, string sortBy, bool isDescending, Expression<Func<TEntity, object>> keySelector)
    {
        return isDescending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
    }
}
