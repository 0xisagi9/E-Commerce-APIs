using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class BrandRepository : BaseRepository<Brand, int>, IBrandRepository
{
    public BrandRepository(DbContext context) : base(context) { }

    public async Task<Brand?> GetByNameAsync(string name) => await _dbSet
            .FirstOrDefaultAsync(b => b.Name == name && !b.IsDeleted);
    public async Task<IEnumerable<Brand>> GetActiveBrandsAsync() => await _dbSet
            .Where(b => !b.IsDeleted)
            .OrderBy(b => b.Name)
            .ToListAsync();
}
