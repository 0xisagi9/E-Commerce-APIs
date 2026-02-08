namespace E_Commerce_APIs.Application.Services;

using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.UnitOfWork;
using E_Commerce_APIs.Shared.Constants;
using E_Commerce_APIs.Shared.Interfaces;

using E_Commerce_APIs.Application.Common.Interfaces;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserRegistrationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

    public async Task AssignRoleAsync(User user, string role)
    {
        var roles = await _unitOfWork.Roles.GetByNameAsync(role);
        if (roles == null)
            throw new InvalidOperationException("Customer role not found");

        var userRole = new UserRole
        {
            RoleId = roles.Id,
            UserId = user.Id,
        };

        await _unitOfWork.UsersRoles.AddAsync(userRole);
    }
}
