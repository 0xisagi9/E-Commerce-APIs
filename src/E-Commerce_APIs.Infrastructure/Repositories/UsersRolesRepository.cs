using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Shared.Interfaces;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class UsersRolesRepository : BaseRepository<UserRole, int>, IUsersRolesRepository
{
    public UsersRolesRepository(AppDbContext context) : base(context) { }
}
