using E_Commerce_APIs.Application.Services;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;

namespace E_Commerce_APIs.Application.Common.Interfaces;

/// <summary>
/// Handles user creation and role assignment logic
/// Separates domain logic from command handler
/// </summary>
public interface IUserRegistrationService
{
    Task<User> CreateUserAsync(string userName, string email, string firstName, string lastName, string phoneNumber, string passwordHash);
    Task AssignCustomerRoleAsync(User user);
}
