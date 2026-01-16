using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using E_Commerce_APIs.Shared.Settings;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;

namespace E_Commerce_APIs.Infrastructure.Services;

public class JWTService : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly byte[] _key;

    public JWTService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
    }
    public string GenerateToken(User user)
    {
        var tokenhandler = new JsonWebTokenHandler();
        var claims = new List<Claim>
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in user.UserRoles)
            claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
        };

        var token = tokenhandler.CreateToken(tokenDescriptor);
        return token;


    }
    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }


}
