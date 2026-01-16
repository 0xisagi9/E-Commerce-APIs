using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IInventoryRepository : IBaseRepository<Inventory, int>
{
    Task<Inventory?> GetByVendorOfferIdAsync(int vendorOfferId);
    Task<int> GetAvailableQuantityAsync(int vendorOfferId);
    Task<bool> ReserveQuantityAsync(int vendorOfferId, int quantity);
    Task<bool> ReleaseReservedQuantityAsync(int vendorOfferId, int quantit);
    Task<bool> ConfirmReservationAsync(int vendorOfferId, int quantity);
    Task<IEnumerable<Inventory>> GetLowStockItemsAsync(int threshold);
}
