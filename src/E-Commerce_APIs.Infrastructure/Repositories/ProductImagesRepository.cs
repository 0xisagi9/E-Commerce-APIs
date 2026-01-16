using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class ProductImagesRepository : BaseRepository<ProductImage, int>, IProductImagesRepository
{
    public ProductImagesRepository(DbContext context) : base(context) { }

    public async Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId) => await _dbSet
            .Where(pi => pi.ProductId == productId)
            .OrderBy(pi => pi.CreationDate)
            .ToListAsync();
}
