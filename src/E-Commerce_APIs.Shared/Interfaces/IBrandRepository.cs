using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IBrandRepository : IBaseRepository<Brand, int>
{
    Task<Brand?> GetByNameAsync(string name);
    Task<IEnumerable<Brand>> GetActiveBrandsAsync();
}
