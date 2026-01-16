

using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IProductRepository : IBaseRepository<Product, int>
{
    Task<Product?> GetByIdWithDetailsAsync(int id);
    Task<Product?> GetBySlugAsync(string slug);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> GetProductsByBrandAsync(int brandId);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task<IEnumerable<Product>> GetFeaturedProductsAsync(int count);
    Task<IEnumerable<Product>> GetActiveProductsAsync();
}
