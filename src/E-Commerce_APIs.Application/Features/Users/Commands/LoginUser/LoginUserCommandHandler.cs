using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;

namespace E_Commerce_APIs.Application.Features.Users.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserLoginService _userLoginService;
    private readonly IRoleService _roleService;
    private readonly IAuthenticationTokenService _authenticationTokenService;
    private readonly IAuthResponseBuilder _authResponseBuilder;
    private readonly ICookieService _cookieService;

    public LoginUserCommandHandler(IUnitOfWork unitOfWork, IUserLoginService userLoginService, IRoleService roleService,
        IAuthenticationTokenService authenticationTokenService, IAuthResponseBuilder authResponseBuilder, ICookieService cookieService)
    {
        _unitOfWork = unitOfWork;
        _userLoginService = userLoginService;
        _roleService = roleService;
        _authenticationTokenService = authenticationTokenService;
        _authResponseBuilder = authResponseBuilder;
        _cookieService = cookieService;
    }

    public async Task<Result<AuthResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Verify user credentials
            var user = await _userLoginService.VerifyCredentialsAsync(request.Email, request.Password);
            if (user == null)
                return Result<AuthResponseDto>.Failure("Invalid email or password", 401);

            // Get user with roles
            var userWithRoles = await _userLoginService.GetUserWithRolesAsync(user.Id);
            if (userWithRoles == null)
                return Result<AuthResponseDto>.Failure("User not found", 404);

            // Get primary role
            var primaryRole = _roleService.GetUserPrimaryRoleName(userWithRoles);
            if (primaryRole == null)
                return Result<AuthResponseDto>.Failure("User has no assigned role", 400);

            // Generate authentication tokens
            var authToken = await _authenticationTokenService.GenerateAuthTokenAsync(userWithRoles, primaryRole, _unitOfWork);

            // Save all changes and commit transaction
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            // Set refresh token cookie
            _authenticationTokenService.SetRefreshTokenCookie(authToken.RefreshToken, authToken.RefreshTokenExpiration, _cookieService);

            // Build and return response
            var response = _authResponseBuilder.BuildAuthResponse(userWithRoles, primaryRole, authToken);

            return Result<AuthResponseDto>.Success(response, "Login successful", 200);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}

