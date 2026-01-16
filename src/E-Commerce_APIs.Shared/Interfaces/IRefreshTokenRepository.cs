
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken, Guid>
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<IEnumerable<RefreshToken>> GetActiveTokensByUserAsync(Guid userId);
    Task RevokeUserTokensAsync(Guid userId);
    Task CleanupExpiredTokensAsync();
}
