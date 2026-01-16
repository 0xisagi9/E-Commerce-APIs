using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IRoleRepository : IBaseRepository<Role, int>
{
    Task<Role?> GetByNameAsync(string name);
    Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId);
}
