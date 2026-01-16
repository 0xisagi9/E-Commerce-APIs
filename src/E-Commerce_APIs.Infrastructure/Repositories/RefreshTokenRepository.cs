using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class RefreshTokenRepository : BaseRepository<RefreshToken, Guid>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext context) : base(context) { }
    public async Task<RefreshToken?> GetByTokenAsync(string token) => await _dbSet
              .Include(rt => rt.User)
              .FirstOrDefaultAsync(rt => rt.Token == token && rt.RevokedAt == null);


    public async Task CleanupExpiredTokensAsync()
    {
        var expiredTokens = await _dbSet
                .Where(rt => rt.ExpiresAt < DateTime.UtcNow.AddDays(-30))
                .ToListAsync();

        _dbSet.RemoveRange(expiredTokens);
    }

    public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserAsync(Guid userId) => await _dbSet
                .Where(rt => rt.UserId == userId
                    && rt.RevokedAt == null
                    && rt.ExpiresAt > DateTime.UtcNow)
                .ToListAsync();


    public async Task RevokeUserTokensAsync(Guid userId)
    {
        var tokens = await _dbSet
               .Where(rt => rt.UserId == userId && rt.RevokedAt == null)
               .ToListAsync();

        foreach (var token in tokens)
        {
            token.RevokedAt = DateTime.UtcNow;
        }
    }
}
