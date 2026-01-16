

using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category, int>
{
    Task<Category?> GetByIdWithChildrenAsync(int id);
    Task<IEnumerable<Category>> GetRootCategoriesAsync();
    Task<IEnumerable<Category>> GetChildCategoriesAsync(int parentId);
    Task<Category?> GetBySlugAsync(string slug);
}
