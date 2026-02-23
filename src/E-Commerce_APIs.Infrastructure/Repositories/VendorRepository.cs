using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class VendorRepository : BaseRepository<Vendor, Guid>, IVendorRepository
{
    public VendorRepository(AppDbContext context) : base(context) { }

    public async Task<Vendor?> GetByEmailAsync(string email) => await _dbSet
            .FirstOrDefaultAsync(v => v.Email == email && !v.IsDeleted);
    public async Task<IEnumerable<Vendor>> GetTopRatedVendorsAsync(int count) => await _dbSet
            .Where(v => !v.IsDeleted)
            .OrderByDescending(v => v.AverageRate)
            .Take(count)
            .ToListAsync();
    public async Task<IEnumerable<Vendor>> GetActiveVendorsAsync() => await _dbSet
            .Where(v => !v.IsDeleted)
            .OrderBy(v => v.Name)
            .ToListAsync();
    public async Task<Vendor?> GetByIdWithOffersAsync(Guid id) => await _dbSet
            .Include(v => v.VendorOffers)
                .ThenInclude(vo => vo.Product)
            .Include(v => v.VendorOffers)
                .ThenInclude(vo => vo.Inventory)
            .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted);

    public async Task<Vendor?> GetByNameAsync(string name) => await _dbSet.FirstOrDefaultAsync(v => v.Name == name);

    public async Task<Vendor?> GetByPhoneNumber(string phoneNumber) => await _dbSet.FirstOrDefaultAsync(v => v.PhoneNumber == phoneNumber);

}
