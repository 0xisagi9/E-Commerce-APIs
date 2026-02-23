using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class ProductCategoryRepository : BaseRepository<ProductCategory, int>, IProductCategoryRepository
{
    public ProductCategoryRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<ProductCategory>> GetCategoriesByProductAsync(int productId) => await _dbSet
        .Include(pc => pc.Category)
        .Where(pc => pc.ProductId == productId)
        .ToListAsync();

    public async Task<IEnumerable<ProductCategory>> GetProductsByCategoryAsync(int categoryId) => await _dbSet
        .Include(pc => pc.Product)
        .Where(pc => pc.CategoryId == categoryId)
        .ToListAsync();
}
