
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IVendorRepository : IBaseRepository<Vendor, Guid>
{
    Task<Vendor?> GetByEmailAsync(string email);
    Task<IEnumerable<Vendor>> GetTopRatedVendorsAsync(int count);
    Task<IEnumerable<Vendor>> GetActiveVendorsAsync();
    Task<Vendor?> GetByIdWithOffersAsync(Guid id);
}
