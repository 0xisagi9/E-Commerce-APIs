using E_Commerce_APIs.Application.Services;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using E_Commerce_APIs.Application.DTOs;


namespace E_Commerce_APIs.Application.Common.Interfaces;

/// <summary>
/// Handles authentication token generation and refresh token creation
/// Separates token logic from command handler
/// </summary>
public interface IAuthenticationTokenService
{
    Task<AuthTokenDto> GenerateAuthTokenAsync(User user, string roleName, IUnitOfWork unitOfWork);
    void SetRefreshTokenCookie(string refreshToken, DateTime expiration, ICookieService cookieService);
}
