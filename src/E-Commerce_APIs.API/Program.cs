using AutoMapper;
using E_Commerce_APIs.API.Configurations;
using E_Commerce_APIs.Infrastructure.Persistence.Context;
using E_Commerce_APIs.Infrastructure.Persistence.UnitOfWork;
using E_Commerce_APIs.Infrastructure.Services;
using E_Commerce_APIs.Shared.Interfaces;
using E_Commerce_APIs.Shared.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


// Load Environment-Specific configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();









builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen();

// Add Infrastructure layer (DbContext, Repositories, Unit of Work, JWT)
builder.Services.AddInfrastructure(builder.Configuration);


// Add MediatR and CQRS
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Add AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
