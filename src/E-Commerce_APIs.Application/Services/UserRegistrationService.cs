namespace E_Commerce_APIs.Application.Services;

using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.UnitOfWork;
using E_Commerce_APIs.Shared.Constants;
using E_Commerce_APIs.Shared.Interfaces;

using E_Commerce_APIs.Application.Common.Interfaces;
using System.Linq.Expressions;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserRegistrationService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;

    }

    public async Task<User> CreateUserAsync(string userName, string email, string firstName, string lastName, string phoneNumber, string passwordHash)
    {
        var user = new User
        {
            UserName = userName,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            PasswordHash = passwordHash,
        };

        return await _unitOfWork.Users.AddAsync(user);
    }

    public async Task AssignRoleAsync(User user, IEnumerable<string> roles)
    {

        foreach (var roleName in roles)
        {
            var role = await _unitOfWork.Roles.GetByNameAsync(roleName);
            if (role is null)
                throw new InvalidOperationException("Customer role not found");
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
            };

            await _unitOfWork.UsersRoles.AddAsync(userRole);
        }
    }
}
