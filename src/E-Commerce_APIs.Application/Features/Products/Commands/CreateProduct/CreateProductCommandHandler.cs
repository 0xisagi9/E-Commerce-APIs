using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Domain.Entities;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Shared.Interfaces;
using MediatR;
using AutoMapper;
using ProductEntity = E_Commerce_APIs.Domain.Entities.Product;

namespace E_Commerce_APIs.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Validate brand exists if provided
            if (request.BrandId.HasValue)
            {
                var brand = await _unitOfWork.Brands.GetByIdAsync(request.BrandId.Value);
                if (brand == null || brand.IsDeleted)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<ProductDTO>.Failure("Brand not found or has been deleted", 404);
                }
            }

            // Validate all categories exist
            if (request.CategoryIds.Any())
            {
                foreach (var categoryId in request.CategoryIds)
                {
                    var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
                    if (category == null || category.IsDeleted)
                    {
                        await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                        return Result<ProductDTO>.Failure($"Category with ID {categoryId} not found or has been deleted", 404);
                    }
                }
            }

            // Create product with Feature as JSON string
            var product = new ProductEntity
            {
                Name = request.Name,
                Description = request.Description,
                SmallImage = request.SmallImage,
                BrandId = request.BrandId,
                Feature = request.Feature,  // Store JSON string directly
                ReviewsCount = 0,
                IsDeleted = false,
                CreationDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            product = await _unitOfWork.Products.AddAsync(product);

            // Add product categories
            if (request.CategoryIds.Any())
            {
                foreach (var categoryId in request.CategoryIds)
                {
                    var productCategory = new ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId,
                        CreatedAt = DateTime.UtcNow
                    };
                    await _unitOfWork.ProductCategories.AddAsync(productCategory);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();

            // Fetch the created product with all related data
            var createdProduct = await _unitOfWork.Products.GetByIdWithDetailsAsync(product.Id);

            // Map to DTO with brand and categories
            var productDTO = new ProductDTO
            {
                Id = createdProduct!.Id,
                Name = createdProduct.Name,
                Description = createdProduct.Description ?? string.Empty,
                Brand = createdProduct.Brand?.Name ?? string.Empty,
                SmallImage = createdProduct.SmallImage ?? string.Empty,
                Features = createdProduct.Feature,  // Return JSON string as-is
                Categories = createdProduct.ProductCategories?
                    .Select(pc => pc.Category.DisplayText)
                    .ToList() ?? new List<string>()
            };

            return Result<ProductDTO>.Success(productDTO, "Product created successfully", 201);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
