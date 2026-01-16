

using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product, int>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync() => await _dbSet
                .Where(p => !p.IsDeleted)
                .ToListAsync();
    public async Task<Product?> GetByIdWithDetailsAsync(int id) => await _dbSet
                .Include(p => p.Brand)
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.VendorOffers)
                    .ThenInclude(vo => vo.Vendor)
                .Include(p => p.VendorOffers)
                    .ThenInclude(vo => vo.Inventory)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);



    public async Task<Product?> GetBySlugAsync(string slug) => await _dbSet
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Slug == slug && !p.IsDeleted);


    public async Task<IEnumerable<Product>> GetFeaturedProductsAsync(int count) => await _dbSet
                .Include(p => p.Brand)
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.ReviewsCount)
                .Take(count)
                .ToListAsync();



    public async Task<IEnumerable<Product>> GetProductsByBrandAsync(int brandId) => await _dbSet
                .Where(p => p.BrandId == brandId && !p.IsDeleted)
                .ToListAsync();


    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId) => await _dbSet
                .Include(p => p.ProductCategories)
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId) && !p.IsDeleted)
                .ToListAsync();


    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm) => await _dbSet
                .Include(p => p.Brand)
                .Where(p => (p.Name.Contains(searchTerm) ||
                            (p.Description != null && p.Description.Contains(searchTerm)))
                            && !p.IsDeleted)
                .ToListAsync();

}
