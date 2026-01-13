namespace E_Commerce_APIs.Shared.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }
    Task<int> CompleteAsync();
    void BeginTransaction();
    Task CommitTransactionAsync();
    void RollbackTransaction();
}

