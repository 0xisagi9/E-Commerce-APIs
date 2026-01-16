
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IProductImagesRepository : IBaseRepository<ProductImage, int>
{
    Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId);
}
