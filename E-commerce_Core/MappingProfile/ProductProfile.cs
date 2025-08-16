using AutoMapper;
using E_commerc_Servers.Services.DTO.ProductDto;
using E_commerce_Core.Entityes; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace E_commerce_Core.MappingProfile
{
    public class ProductProfile : Profile // Inherit from AutoMapper's Profile class
    {
        public ProductProfile()
        {
            CreateMap<Product, GetAllProductDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                 .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images));

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.CreatedAtUtc, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAtUtc, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.ReviewCount, opt => opt.MapFrom(_ => 0))
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ImageUrls));

            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.UpdatedAtUtc, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ImageUrls));


        }
    }
}