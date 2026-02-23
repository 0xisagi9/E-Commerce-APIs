using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Services;
using E_Commerce_APIs.Domain.Entities;


namespace E_Commerce_APIs.Application.Common.Interfaces;

/// <summary>
/// Builds the authentication response DTO
/// Separates response mapping logic from command handler
/// </summary>
public interface IAuthResponseBuilder
{
    AuthResponseDto BuildAuthResponse(User user, IEnumerable<string> roleNames, AuthTokenDto authToken);
}

