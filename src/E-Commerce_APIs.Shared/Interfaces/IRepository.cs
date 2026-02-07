using System.Linq.Expressions;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IBaseRepository<TEntity, TKey> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    // For Complex Queries
    IQueryable<TEntity> GetQueryble();
    IQueryable<TEntity> GetQueryble(ISpecification<TEntity> specification);
}