using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using E_Commerce_APIs.Shared.Interfaces;
namespace E_Commerce_APIs.Infrastructure.Persistence.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;
    // User Domain
    private IUserRepository? _users;
    private IUserAddressRepository? _userAddresses;
    private IRoleRepository? _roles;
    private IRefreshTokenRepository? _refreshTokens;

    // Product Catalog Domain
    private IProductRepository? _products;
    private ICategoryRepository? _categories;
    private IBrandRepository? _brands;
    private IProductImagesRepository? _productImages;

    // Vendor & Inventory Domain
    private IVendorRepository? _vendors;
    private IVendorOfferRepository? _vendorOffers;
    private IInventoryRepository? _inventory;

    public UnitOfWork(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // User Domain Properties
    public IUserRepository Users =>
        _users ??= new UserRepository(_context);

    public IUserAddressRepository UserAddresses =>
        _userAddresses ??= new UserAddressRepository(_context);

    public IRoleRepository Roles =>
        _roles ??= new RoleRepository(_context);

    public IRefreshTokenRepository RefreshTokens =>
        _refreshTokens ??= new RefreshTokenRepository(_context);
    // Product Catalog Domain Properties
    public IProductRepository Products =>
        _products ??= new ProductRepository(_context);

    public ICategoryRepository Categories =>
        _categories ??= new CategoryRepository(_context);

    public IBrandRepository Brands =>
        _brands ??= new BrandRepository(_context);

    public IProductImagesRepository ProductImages =>
        _productImages ??= new ProductImagesRepository(_context);

    // Vendor & Inventory Domain Properties
    public IVendorRepository Vendors =>
        _vendors ??= new VendorRepository(_context);

    public IVendorOfferRepository VendorOffers =>
        _vendorOffers ??= new VendorOfferRepository(_context);

    public IInventoryRepository Inventory =>
        _inventory ??= new InventoryRepository(_context);

    //Transaction Management
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);

            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
    }
}
