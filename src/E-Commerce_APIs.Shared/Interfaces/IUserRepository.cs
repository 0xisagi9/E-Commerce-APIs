using E_Commerce_APIs.Domain.Entities;
namespace E_Commerce_APIs.Shared.Interfaces;

public interface IUserRepository : IRepository<User, Guid>
{
}
