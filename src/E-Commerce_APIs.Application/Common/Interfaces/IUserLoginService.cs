namespace E_Commerce_APIs.Application.Common.Interfaces;

using E_Commerce_APIs.Domain.Entities;

/// <summary>
/// Handles user login and credential verification logic
/// Separates authentication logic from command handler
/// </summary>
public interface IUserLoginService
{
    /// <summary>
    /// Verifies user credentials (email and password)
    /// </summary>
    Task<User?> VerifyCredentialsAsync(string email, string password);

    /// <summary>
    /// Gets user with their roles
    /// </summary>
    Task<User?> GetUserWithRolesAsync(Guid userId);
}
