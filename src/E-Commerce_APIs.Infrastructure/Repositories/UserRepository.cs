using E_Commerce_APIs.Shared.Interfaces;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }
    public async Task<User?> GetByEmailAsync(string email) => await _dbSet.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
    public async Task<User?> GetByUserNameAsync(string userName) => await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName && !u.IsDeleted);
    public async Task<User?> GetByIdWithRolesAsync(Guid id) => await _dbSet.Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
    public async Task<IEnumerable<User>> GetActiveUsersAsync() => await _dbSet.Where(u => !u.IsDeleted).OrderByDescending(_ => _.CreatedAt).ToListAsync();
    public async Task<IEnumerable<User>> GetVerifiedUsersAsync() => await _dbSet.Where(u => u.IsVerified && !u.IsDeleted).OrderByDescending(_ => _.CreatedAt).ToListAsync();
    public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null)
    {
        var query = _dbSet.Where(u => u.Email == email && !u.IsDeleted);
        if (excludeUserId.HasValue)
            query = query.Where(u => u.Id != excludeUserId.Value);
        return !await query.AnyAsync();
    }

}
