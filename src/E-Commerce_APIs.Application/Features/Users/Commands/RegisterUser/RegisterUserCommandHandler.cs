using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Constants;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRegistrationService _userRegistrationService;
    private readonly IAuthenticationTokenService _authenticationTokenService;
    private readonly IAuthResponseBuilder _authResponseBuilder;
    private readonly ICookieService _cookieService;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserRegistrationService userRegistrationService,
          IAuthenticationTokenService authenticationTokenService, IAuthResponseBuilder authResponseBuilder, ICookieService cookieService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _userRegistrationService = userRegistrationService;
        _authenticationTokenService = authenticationTokenService;
        _authResponseBuilder = authResponseBuilder;
        _cookieService = cookieService;
    }

    public async Task<Result<AuthResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Create user
            var user = await _userRegistrationService.CreateUserAsync(
                request.UserName,
                request.Email,
                request.FirstName,
                request.LastName,
                request.PhoneNumber ?? string.Empty, // Ensure non-null value
                _passwordHasher.HashPassword(request.Password)
            );

            // Assign customer role
            await _userRegistrationService.AssignRoleAsync(user, new[] { Roles.Customer });

            // Generate authentication tokens
            var authToken = await _authenticationTokenService.GenerateAuthTokenAsync(user, new[] { Roles.Customer }, _unitOfWork);

            // Save all changes and commit transaction
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            // Set refresh token cookie
            _authenticationTokenService.SetRefreshTokenCookie(authToken.RefreshToken, authToken.RefreshTokenExpiration, _cookieService);

            // Build and return response
            var response = _authResponseBuilder.BuildAuthResponse(user, new[] { Roles.Customer }, authToken);

            return Result<AuthResponseDto>.Success(response, "Registration successful, please verify your email", 201);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
