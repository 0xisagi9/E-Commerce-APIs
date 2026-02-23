namespace E_Commerce_APIs.Shared.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // User Domain
    IUserRepository Users { get; }
    IUserAddressRepository UserAddresses { get; }
    IRoleRepository Roles { get; }
    IUsersRolesRepository UsersRoles { get; }
    IRefreshTokenRepository RefreshTokens { get; }

    // Product Catalog Domain
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IBrandRepository Brands { get; }
    IProductImagesRepository ProductImages { get; }
    IProductCategoryRepository ProductCategories { get; }

    // Vendor & Inventory Domain
    IVendorRepository Vendors { get; }
    IVendorOfferRepository VendorOffers { get; }
    IInventoryRepository Inventory { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}

