

using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IVendorOfferRepository : IBaseRepository<VendorOffer, int>
{
    Task<IEnumerable<VendorOffer>> GetOffersByProductAsync(int productId);
    Task<IEnumerable<VendorOffer>> GetOffersByVendorAsync(Guid vendorId);
    Task<VendorOffer?> GetCheapestOfferAsync(int productId);
    Task<VendorOffer?> GetByIdWithInventoryAsync(int id);
    Task<bool> HasActiveOffersAsync(int productId);
}
