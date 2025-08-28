using AutoMapper;
using E_commerce_Core.DTO.ReviewDtos;
using E_commerce_Core.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.MappingProfile
{
    public  class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment ?? string.Empty))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdataReviewDto, Review>()
               .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment ?? string.Empty))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.ReviewId, op => op.MapFrom(src => src.ReviewId));


            CreateMap<Review, ReviewResponesDto>()
                .ForMember(d => d.ProductName, op => op.MapFrom(s => s.Product.Name ))
                .ForMember(d => d.ReviewId, op => op.MapFrom(s => s.ReviewId))
                .ForMember(d => d.UserName, op => op.MapFrom(s => s.User.UserName));

            CreateMap<Review, UserReviewDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ReviewId))
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<Product, ProductReviewDtos>()
                .ForMember(d => d.Reviews, op => op.MapFrom(s => s.Reviews))
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0));

        }
    }
}
