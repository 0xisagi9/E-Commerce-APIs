using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);
    public string GenerateRefreshToken();
}
