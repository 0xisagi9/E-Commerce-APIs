using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IProductCategoryRepository : IBaseRepository<ProductCategory, int>
{
    Task<IEnumerable<ProductCategory>> GetCategoriesByProductAsync(int productId);
    Task<IEnumerable<ProductCategory>> GetProductsByCategoryAsync(int categoryId);
}
