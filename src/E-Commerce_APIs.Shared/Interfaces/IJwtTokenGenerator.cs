using E_Commerce_APIs.Domain.Entities;
using System.Security.Claims;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user, IEnumerable<string> role);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<RefreshToken> CreateRefreshTokenAsync(Guid userId, string token, string ipAddress, string userAgent);
}
