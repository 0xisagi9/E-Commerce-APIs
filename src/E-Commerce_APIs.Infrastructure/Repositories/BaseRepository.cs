using E_Commerce_APIs.Shared.Interfaces;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class BaseRepository<TEntity, Tkey> : IBaseRepository<TEntity, Tkey> where TEntity : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }


    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public virtual Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }
    public virtual Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Tkey id) => await _dbSet.FindAsync(id);
    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.AnyAsync(predicate);
    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();
    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null) => predicate == null
        ? await _dbSet.CountAsync() : await _dbSet.CountAsync(predicate);

    // For Complex Queries
    public IQueryable<TEntity> GetQueryble() => _dbSet.AsQueryable();

    public IQueryable<TEntity> GetQueryble(ISpecification<TEntity> specification) => _dbSet.Where(specification.Criteria);

}
