
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IVendorRepository : IBaseRepository<Vendor, Guid>
{
    Task<Vendor?> GetByEmailAsync(string email);
    Task<Vendor?> GetByNameAsync(string name);
    Task<Vendor?> GetByPhoneNumber(string phoneNumber);
    Task<IEnumerable<Vendor>> GetTopRatedVendorsAsync(int count);
    Task<IEnumerable<Vendor>> GetActiveVendorsAsync();
    Task<Vendor?> GetByIdWithOffersAsync(Guid id);
}
