using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Common.Interfaces;


namespace E_Commerce_APIs.Application.Services;
public class AuthenticationTokenService : IAuthenticationTokenService
{
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationTokenService(IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
    {
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthTokenDto> GenerateAuthTokenAsync(User user, IEnumerable<string> roles, IUnitOfWork unitOfWork)
    {
        var accessToken = _jwtService.GenerateAccessToken(user, roles);
        var refreshToken = _jwtService.GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

        // Extract HTTP context information
        var httpContext = _httpContextAccessor.HttpContext;
        var remoteIPAddress = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
        var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString() ?? "Unknown";

        // Create refresh token entity
        var refreshTokenEntity = await _jwtService.CreateRefreshTokenAsync(user.Id, refreshToken, remoteIPAddress, userAgent);
        await unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);

        return new AuthTokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiration = refreshTokenExpiration,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15)
        };
    }

    public void SetRefreshTokenCookie(string refreshToken, DateTime expiration, ICookieService cookieService)
    {
        cookieService.SetRefreshToken(refreshToken, expiration);
    }
}


