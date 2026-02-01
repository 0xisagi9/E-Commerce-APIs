using AutoMapper;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Infrastructure.Persistence.UnitOfWork;
using E_Commerce_APIs.Infrastructure.Repositories;
using E_Commerce_APIs.Infrastructure.Services;
using E_Commerce_APIs.Shared.Constants;
using E_Commerce_APIs.Shared.Interfaces;
using E_Commerce_APIs.Shared.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace E_Commerce_APIs.API.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));


        // JWT Settings
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        // Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false; // Set to true in production
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings?.SecretKey ?? string.Empty))
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var result = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        error = "Unauthorized",
                        message = "You are not authorized to access this resource"
                    });

                    return context.Response.WriteAsync(result);
                }
            };
        });

        // Authorization Policies
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.AdminOnly, policy =>
                policy.RequireRole(Roles.Admin));

            options.AddPolicy(Policies.CustomerOrAdmin, policy =>
                policy.RequireRole(
                    Roles.Customer,
                    Roles.Admin));

            options.AddPolicy(Policies.VendorOrAdmin, policy =>
                policy.RequireRole(
                   Roles.Vendor,
                    Roles.Admin));

            options.AddPolicy(Policies.ManagerOrAdmin, policy =>
                policy.RequireRole(
                    Roles.Manager,
                   Roles.Admin));
        });

        // Password Hasher
        services.AddScoped<IPasswordHasher, PasswordHasher>();


        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // User Domain Repositories
        services.AddScoped<IUserAddressRepository, UserAddressRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUsersRolesRepository, UsersRolesRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        //// Product Catalog Domain Repositories
        //services.AddScoped<IProductRepository, ProductRepository>();
        //services.AddScoped<ICategoryRepository, CategoryRepository>();
        //services.AddScoped<IBrandRepository, BrandRepository>();
        //services.AddScoped<IProductImagesRepository, ProductImagesRepository>();

        //// Vendor & Inventory Domain Repositories
        //services.AddScoped<IVendorRepository, VendorRepository>();
        //services.AddScoped<IVendorOfferRepository, VendorOfferRepository>();
        //services.AddScoped<IInventoryRepository, InventoryRepository>();


        //JWT Services 
        services.AddScoped<IJwtService, JWTService>();

        services.AddScoped<ICookieService, CookieService>();

        services.AddHttpContextAccessor();


        return services;

    }
}