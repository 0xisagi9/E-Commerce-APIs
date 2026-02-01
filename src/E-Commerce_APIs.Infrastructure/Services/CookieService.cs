using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.AspNetCore.Http;

namespace E_Commerce_APIs.Infrastructure.Services;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string RefreshTokenCookieName = "refreshToken";

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {


        _httpContextAccessor = httpContextAccessor;
    }
    public void SetRefreshToken(string refreshToken, DateTime expiredAt)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = expiredAt,
            Path = "/",
            IsEssential = true,
        };

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(RefreshTokenCookieName, refreshToken, cookieOptions);
    }
    public string? GetRefreshToken()
    {
        if (_httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(RefreshTokenCookieName, out var refreshToken) == true)
        {
            return refreshToken;
        }
        return null;
    }
    public void DeleteRefreshToken()
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(-1), // Expire immediately
            Path = "/"
        };

        _httpContextAccessor.HttpContext?.Response.Cookies
            .Append(RefreshTokenCookieName, "", cookieOptions);
    }




}
