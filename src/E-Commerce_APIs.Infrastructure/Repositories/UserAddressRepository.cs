using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class UserAddressRepository : BaseRepository<UserAddress, int>, IUserAddressRepository
{
    public UserAddressRepository(AppDbContext context) : base(context) { }

    public async Task<UserAddress?> GetDefaultAddressAsync(Guid userId) => await _dbSet
                .Where(ua => ua.UserId == userId)
                .OrderByDescending(ua => ua.CreationDate)
                .FirstOrDefaultAsync();

    public async Task<IEnumerable<UserAddress>> GetUserAddressesAsync(Guid userId) => await _dbSet
                .Where(ua => ua.UserId == userId)
                .OrderByDescending(ua => ua.CreationDate)
                .ToListAsync();
}
