using AutoMapper;
using E_commerce_Core.DTO.PaymentDtos;
using E_commerce_Core.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.MappingProfile
{
    public  class PaymentProfile:Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment,PaymentResponseDTO>();

            CreateMap<PaymentRequestDTO,Payment>();

            CreateMap<PaymentStatusUpdateDTO, Payment>()
             .ForMember(dest => dest.paymentStatus, opt => opt.MapFrom(src => src.Status))
             .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId));

        }
    }
}
