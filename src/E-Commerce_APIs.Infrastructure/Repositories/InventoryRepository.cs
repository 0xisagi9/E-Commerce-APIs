
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class InventoryRepository : BaseRepository<Inventory, int>, IInventoryRepository
{
    public InventoryRepository(AppDbContext context) : base(context) { }

    public async Task<Inventory?> GetByVendorOfferIdAsync(int vendorOfferId) => await _dbSet
            .Include(i => i.VendorOffer)
                .ThenInclude(vo => vo.Product)
            .FirstOrDefaultAsync(i => i.VendorOfferId == vendorOfferId);


    public async Task<int> GetAvailableQuantityAsync(int vendorOfferId)
    {
        var inventory = await GetByVendorOfferIdAsync(vendorOfferId);
        return inventory != null ? inventory.Quantity - inventory.ReservedQuantity : 0;
    }

    public async Task<bool> ReserveQuantityAsync(int vendorOfferId, int quantity)
    {
        var inventory = await GetByVendorOfferIdAsync(vendorOfferId);

        if (inventory == null || (inventory.Quantity - inventory.ReservedQuantity) < quantity)
            return false;

        inventory.ReservedQuantity += quantity;
        inventory.ModifiedDate = DateTime.UtcNow;
        return true;
    }

    public async Task<bool> ReleaseReservedQuantityAsync(int vendorOfferId, int quantity)
    {
        var inventory = await GetByVendorOfferIdAsync(vendorOfferId);

        if (inventory == null || inventory.ReservedQuantity < quantity)
            return false;

        inventory.ReservedQuantity -= quantity;
        inventory.ModifiedDate = DateTime.UtcNow;
        return true;
    }

    public async Task<bool> ConfirmReservationAsync(int vendorOfferId, int quantity)
    {
        var inventory = await GetByVendorOfferIdAsync(vendorOfferId);

        if (inventory == null || inventory.ReservedQuantity < quantity)
            return false;

        inventory.ReservedQuantity -= quantity;
        inventory.Quantity -= quantity;
        inventory.ModifiedDate = DateTime.UtcNow;
        return true;
    }

    public async Task<IEnumerable<Inventory>> GetLowStockItemsAsync(int threshold) => await _dbSet
            .Include(i => i.VendorOffer)
                .ThenInclude(vo => vo.Product)
            .Where(i => (i.Quantity - i.ReservedQuantity) <= threshold)
            .ToListAsync();
}
