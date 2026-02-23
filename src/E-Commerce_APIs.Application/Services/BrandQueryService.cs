using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Common.Interfaces;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

namespace E_Commerce_APIs.Application.Services;

public class BrandQueryService
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IGenericSortService<Brand> _sortService;
    protected readonly IMapper _mapper;

    public BrandQueryService(IUnitOfWork unitOfWork, IGenericSortService<Brand> sortService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _sortService = sortService;
        _mapper = mapper;
    }

    protected IBrandRepository GetRepository() => _unitOfWork.Brands;

    protected BrandDTO MapToDto(Brand brand) => _mapper.Map<BrandDTO>(brand);

    protected List<BrandDTO> MapToDtos(List<Brand> brands) => _mapper.Map<List<BrandDTO>>(brands);

    public async Task<(List<Brand> items, int totalCount)> GetEntitiesAsync(
        Expression<Func<Brand, bool>>? specification,
        int skip,
        int take,
        string? sortBy,
        string? sortOrder,
        Func<IQueryable<Brand>, IQueryable<Brand>>? includeFunc = null)
    {
        var repository = GetRepository();
        var query = repository.GetQueryble();

        if (specification != null)
            query = query.Where(specification);

        var totalCount = await query.CountAsync();

        query = _sortService.ApplySorting(query, sortBy, sortOrder);

        if (includeFunc != null)
            query = includeFunc(query);

        var items = await query.Skip(skip).Take(take).ToListAsync();

        return (items, totalCount);
    }

    public async Task<Brand?> GetByIdAsync(int id)
    {
        var repository = GetRepository();
        return await repository.GetByIdAsync(id);
    }

    public PaginatedResult<BrandDTO> CreatePaginatedResult(List<BrandDTO> items, int pageNumber, int pageSize, int totalCount)
    {
        return new PaginatedResult<BrandDTO>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalCount == 0 ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<BrandDTO?> GetBrandByIdAsync(int id)
    {
        var brand = await GetByIdAsync(id);
        return brand == null ? null : MapToDto(brand);
    }
}
