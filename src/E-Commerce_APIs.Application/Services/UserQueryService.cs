using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

namespace E_Commerce_APIs.Application.Services;

public class UserQueryService : GenericQueryServiceBase<User, UserDto, IUserRepository>
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public UserQueryService(IUnitOfWork unitOfWork, IGenericSortService<User> sortService, IRoleService roleService, IMapper mapper)
        : base(unitOfWork, sortService)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    protected override IUserRepository GetRepository() => _unitOfWork.Users;

    protected override UserDto MapToDto(User user)
    {
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Roles = _roleService.GetUserRoleNames(user).ToList();
        return userDto;
    }

    public async Task<(List<User> users, int totalCount)> GetUsersWithRolesAsync(
        Expression<Func<User, bool>>? specification,
        int skip, int take, string? sortBy, string? sortOrder)
    {
        return await GetEntitiesAsync(specification, skip, take, sortBy, sortOrder,
            query => query.Include(u => u.UserRoles).ThenInclude(ur => ur.Role));
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await GetByIdAsync(id,
            query => query.Include(u => u.UserRoles).ThenInclude(ur => ur.Role));
        return user == null ? null : MapToDto(user);
    }
}
