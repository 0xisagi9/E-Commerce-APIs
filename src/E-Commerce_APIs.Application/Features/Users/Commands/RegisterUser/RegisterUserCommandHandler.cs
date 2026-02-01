using AutoMapper;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.UnitOfWork;
using E_Commerce_APIs.Shared.Constants;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using FluentValidation.Results;
using FluentValidation;


namespace E_Commerce_APIs.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICookieService _cookieService;
    private readonly IValidator<RegisterUserCommand> _validator;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper, IPasswordHasher passwordHasher,
        IJwtService jwtService,
        IHttpContextAccessor httpContextAccessor, ICookieService cookieService, IValidator<RegisterUserCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
        _cookieService = cookieService;
        _validator = validator;
    }

    public async Task<Result<AuthResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Create User
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
            };
            user = await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Assign Role For User
            var role = await _unitOfWork.Roles.GetByNameAsync(Roles.Customer);
            var userRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            };
            userRole = await _unitOfWork.UsersRoles.AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync();

            var accessToken = _jwtService.GenerateAccessToken(user, role.Name);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);
            var httpContext = _httpContextAccessor.HttpContext;
            var remoteIPAddress = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
            var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString() ?? "Unknown";
            var refreshTokenEntity = await _jwtService.CreateRefreshTokenAsync(user.Id, refreshToken, remoteIPAddress, userAgent);

            await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();

            _cookieService.SetRefreshToken(refreshToken, refreshTokenExpiration);

            var response = new AuthResponseDto
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
                    Roles = new List<string> { Roles.Customer }
                },
                AccessToken = accessToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            };

            return Result<AuthResponseDto>.Success(response, "Registration successful, please verify your email", 201);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<AuthResponseDto>.Failure("Registration failed", 400, null);
        }
    }
}
