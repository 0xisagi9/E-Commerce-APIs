using E_Commerce_APIs.Shared.Interfaces;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace E_Commerce_APIs.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

}
