namespace E_Commerce_APIs.Shared.Interfaces;

public interface ICookieService
{
    void SetRefreshToken(string refreshToken, DateTime expiredAt);
    string GetRefreshToken();
    void DeleteRefreshToken();
}
