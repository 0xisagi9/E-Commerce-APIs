using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class VendorOfferRepository : BaseRepository<VendorOffer, int>, IVendorOfferRepository
{
    public VendorOfferRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<VendorOffer>> GetOffersByProductAsync(int productId) => await _dbSet
            .Include(vo => vo.Vendor)
            .Include(vo => vo.Inventory)
            .Where(vo => vo.ProductId == productId && !vo.IsDeleted)
            .OrderBy(vo => vo.Price)
            .ToListAsync();


    public async Task<IEnumerable<VendorOffer>> GetOffersByVendorAsync(Guid vendorId) => await _dbSet
            .Include(vo => vo.Product)
            .Include(vo => vo.Inventory)
            .Where(vo => vo.VendorId == vendorId && !vo.IsDeleted)
            .ToListAsync();


    public async Task<VendorOffer?> GetCheapestOfferAsync(int productId) => await _dbSet
            .Include(vo => vo.Vendor)
            .Include(vo => vo.Inventory)
            .Where(vo => vo.ProductId == productId && !vo.IsDeleted)
            .OrderBy(vo => vo.Price)
            .FirstOrDefaultAsync();

    public async Task<VendorOffer?> GetByIdWithInventoryAsync(int id) => await _dbSet
            .Include(vo => vo.Vendor)
            .Include(vo => vo.Product)
            .Include(vo => vo.Inventory)
            .FirstOrDefaultAsync(vo => vo.Id == id && !vo.IsDeleted);
    public async Task<bool> HasActiveOffersAsync(int productId) => await _dbSet
            .AnyAsync(vo => vo.ProductId == productId && !vo.IsDeleted);
}
