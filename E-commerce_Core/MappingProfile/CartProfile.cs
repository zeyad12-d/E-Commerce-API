using AutoMapper;
using E_commerce_Core.DTO.CartDtos;
using E_commerce_Core.DTO.CategoryDTOs;
using E_commerce_Core.Entityes;

namespace E_commerce_Core.MappingProfile
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            // output dto
            CreateMap<ShoppingCart, CartResponesDto>()
             .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
             .ForMember(d=> d.UserName, o=>o.MapFrom(s=>s.User.UserName));
            

            CreateMap <CartItem,CartitemResponesDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Price * src.Quantity))
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
                .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(src => src.CartItemId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<Product, ProductResponseDto>();
                
            



        }
    }
    
}

