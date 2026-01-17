using E_Commerce_APIs.Domain.Entities;
namespace E_Commerce_APIs.Shared.Interfaces;

public interface IUserRepository : IBaseRepository<User, Guid>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string userName);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetByIdWithRolesAsync(Guid id);
    Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null);
    Task<IEnumerable<User>> GetVerifiedUsersAsync();
    Task<IEnumerable<User>> GetActiveUsersAsync();
}
