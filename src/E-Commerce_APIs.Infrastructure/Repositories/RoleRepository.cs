using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace E_Commerce_APIs.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context) { }

        public async Task<Role?> GetByNameAsync(string name) => await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
        public async Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId) => await _context.Set<UserRole>()
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToListAsync();
    }
}
