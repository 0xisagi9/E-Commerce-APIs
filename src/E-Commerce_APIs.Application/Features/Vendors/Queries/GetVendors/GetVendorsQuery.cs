using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_APIs.Application.Features.Vendors.Queries.GetVendors;

public class GetVendorsQuery : IRequest<PaginatedResult<VendorDTO>>
{
    public bool isDeleted { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; } = "created_at";
    public string? SortOrder { get; set; } = "desc";
}
