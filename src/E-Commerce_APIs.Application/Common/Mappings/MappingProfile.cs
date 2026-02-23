using AutoMapper;
using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Brand Mappings
        CreateMap<Brand, BrandDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug));

        CreateMap<BrandDTO, Brand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug));

        // Product Mappings
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? string.Empty))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : string.Empty))
            .ForMember(dest => dest.SmallImage, opt => opt.MapFrom(src => src.SmallImage ?? string.Empty))
            .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Feature))
            .ForMember(dest => dest.Categories, opt => opt.Ignore());

        CreateMap<ProductDTO, Product>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.SmallImage, opt => opt.MapFrom(src => src.SmallImage))
            .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Features));

        // Vendor Mappings
        CreateMap<Vendor, VendorDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.WebsiteUrl, opt => opt.MapFrom(src => src.WebsiteUrl))
            .ForMember(dest => dest.AverageRate, opt => opt.MapFrom(src => src.AverageRate))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug));

        CreateMap<VendorDTO, Vendor>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.WebsiteUrl, opt => opt.MapFrom(src => src.WebsiteUrl))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug));

        // User Mappings (without roles - roles are handled separately)
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.IsVerified))
            .ForMember(dest => dest.Roles, opt => opt.Ignore());

        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
    }
}
