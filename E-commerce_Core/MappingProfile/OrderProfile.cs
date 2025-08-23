using AutoMapper;
using E_commerce_Core.DTO.OrderDtos;
using E_commerce_Core.Entityes;

namespace E_commerce_Core.MappingProfile
{
    public class OrderProfile:Profile
    {

        public OrderProfile()
        {
            CreateMap<CreateOrderDto, Order>()
                 .ForMember(o => o.UserName, op => op.MapFrom(s => s.UserName))
              
                 .ForMember(d => d.Items, opt => opt.MapFrom(src => src.OrderItems))
                 .ForMember(d => d.TotalAmount, opt => opt.MapFrom(src => src.OrderItems.Sum(i => i.Quantity * 0)));

            CreateMap<CreateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.Ignore());


            CreateMap<Order, OrderResponseDto>()
       .ForMember(d => d.Id, opt => opt.MapFrom(src => src.OrderId))
        .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.UserName))
      .ForMember(d => d.ShippingAddress, o => o.MapFrom(s => s.ShippingAddress))
      .ForMember(d => d.BillingAddress, o => o.MapFrom(s => s.BillingAddress))
      .ForMember(d => d.OrderDate, o => o.MapFrom(s => s.CreatedAt))
      .ForMember(d => d.TotalAmount, o => o.MapFrom(s => s.TotalAmount))
      .ForMember(d => d.OrderItems, opt => opt.MapFrom(src => src.Items))
       .ForMember(d => d.price, opt => opt.MapFrom(src => src.Items.Sum(i => i.Price * i.Quantity)));


            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));






        }
    }
}
