

using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
{
    public CategoryRepository(DbContext context) : base(context) { }
    public async Task<Category?> GetByIdWithChildrenAsync(int id) => await _dbSet
            .Include(c => c.InverseParent)
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
    public async Task<IEnumerable<Category>> GetRootCategoriesAsync() => await _dbSet
            .Where(c => c.ParentId == null && !c.IsDeleted)
            .Include(c => c.InverseParent)
            .ToListAsync();
    public async Task<IEnumerable<Category>> GetChildCategoriesAsync(int parentId) => await _dbSet
            .Where(c => c.ParentId == parentId && !c.IsDeleted)
            .ToListAsync();
    public async Task<Category?> GetBySlugAsync(string slug) => await _dbSet
            .FirstOrDefaultAsync(c => c.Slug == slug && !c.IsDeleted);
}
