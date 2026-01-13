using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using E_Commerce_APIs.Shared.Interfaces;
namespace E_Commerce_APIs.Infrastructure.Persistence.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    private IUserRepository? _userRepository;
    public IUserRepository User => _userRepository ??= new UserRepository(_context);

    public UnitOfWork(AppDbContext context) => _context = context;
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();

    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();

    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public void RollbackTransaction()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }

    private async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
