namespace E_Commerce_APIs.Application.Services;

using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Constants;
using E_Commerce_APIs.Application.Common.Interfaces;


public class AuthResponseBuilder : IAuthResponseBuilder
{
    public AuthResponseDto BuildAuthResponse(User user, string roleName, AuthTokenDto authToken)
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
                Roles = new List<string> { roleName }
            },
            AccessToken = authToken.AccessToken,
            ExpiresAt = authToken.ExpiresAt
        };
    }
}
