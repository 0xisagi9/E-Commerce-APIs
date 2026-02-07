using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using Npgsql.Replication;

namespace E_Commerce_APIs.Application.Features.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<PaginatedResult<UserDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public bool? IsVerified { get; set; }
    public bool? IsDeleted { get; set; }
    public int? RoleId { get; set; }

    public string? SortBy { get; set; } = "created_at";
    public string? SortOrder { get; set; } = "desc";
}
