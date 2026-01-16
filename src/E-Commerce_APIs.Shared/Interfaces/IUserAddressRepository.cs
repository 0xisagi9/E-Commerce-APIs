using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IUserAddressRepository : IBaseRepository<UserAddress, int>
{
    Task<IEnumerable<UserAddress>> GetUserAddressesAsync(Guid userId);
    Task<UserAddress?> GetDefaultAddressAsync(Guid userId);
}
