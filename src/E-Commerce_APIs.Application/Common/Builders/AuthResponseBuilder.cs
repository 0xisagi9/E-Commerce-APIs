using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Application.DTOs;

namespace E_Commerce_APIs.Application.Common.Builders;

public class AuthResponseBuilder : IAuthResponseBuilder
{
    public AuthResponseDto BuildAuthResponse(User user, IEnumerable<string> roleNames, AuthTokenDto authToken)
    {
        return new AuthResponseDto
        {
            User = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                IsVerified = user.IsVerified,
                Roles = roleNames.ToList()
            },
            AccessToken = authToken.AccessToken,
            ExpiresAt = authToken.ExpiresAt
        };
    }
}

