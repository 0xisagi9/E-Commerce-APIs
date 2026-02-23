using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

namespace E_Commerce_APIs.Application.Services;

public class VendorQueryService : GenericQueryServiceBase<Vendor, VendorDTO, IVendorRepository>
{
    private readonly IMapper _mapper;

    public VendorQueryService(IUnitOfWork unitOfWork, IGenericSortService<Vendor> sortService, IMapper mapper)
        : base(unitOfWork, sortService)
    {
        _mapper = mapper;
    }

    protected override IVendorRepository GetRepository() => _unitOfWork.Vendors;

    protected override VendorDTO MapToDto(Vendor vendor) => _mapper.Map<VendorDTO>(vendor);

    public async Task<(List<Vendor> vendors, int totalCount)> GetVendorsWithOffersAsync(
        Expression<Func<Vendor, bool>>? specification,
        int skip, int take, string? sortBy, string? sortOrder)
    {
        return await GetEntitiesAsync(specification, skip, take, sortBy, sortOrder,
            query => query.Include(v => v.VendorOffers).ThenInclude(vo => vo.Product));
    }

    public async Task<VendorDTO?> GetVendorByIdAsync(Guid id)
    {
        var vendor = await GetByIdAsync(id,
            query => query.Include(v => v.VendorOffers).ThenInclude(vo => vo.Product));
        return vendor == null ? null : MapToDto(vendor);
    }
}
