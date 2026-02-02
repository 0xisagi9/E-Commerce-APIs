using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;

namespace E_Commerce_APIs.Application.Services;

/// <summary>
/// Handles user login and credential verification
/// Delegates to repositories for data access
/// </summary>
public class UserLoginService : IUserLoginService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserLoginService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> VerifyCredentialsAsync(string email, string password)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);
        if (user == null)
            return null;

        if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
            return null;

        return user;
    }

    public async Task<User?> GetUserWithRolesAsync(Guid userId)
    {
        return await _unitOfWork.Users.GetByIdWithRolesAsync(userId);
    }
}
