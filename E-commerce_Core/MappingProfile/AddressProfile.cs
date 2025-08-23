using AutoMapper;
using E_commerce_Core.DTO.AddresDtos;
using E_commerce_Core.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.MappingProfile
{
    public class AddressProfile:Profile
    {

        public AddressProfile()
        {
            CreateMap<CreateAddressDto, Address>()
                .ForMember(D => D.AddressLine1, op => op.MapFrom(s => s.AddressLine1))
                .ForMember(d => d.AddressLine2, OP => OP.MapFrom(s => s.AddressLine2))
                .ForMember(D => D.City, op => op.MapFrom(s => s.City))
                .ForMember(d => d.State, op => op.MapFrom(s => s.State))
                .ForMember(d => d.Country, op => op.MapFrom(s => s.Country))
                .ForMember(d => d.PostalCode, op => op.MapFrom(s => s.PostalCode))
                .ForPath(d => d.user.UserName, op => op.MapFrom(s => s.UserName));

            CreateMap<Address, AddressResponseDto>()
                .ForMember(d=>d.Id, op => op.MapFrom(s => s.Id))
                .ForMember(d => d.AddressLine1, op => op.MapFrom(s => s.AddressLine1))
                .ForMember(d => d.AddressLine2, op => op.MapFrom(s => s.AddressLine2))
                .ForMember(d => d.City, op => op.MapFrom(s => s.City))
                .ForMember(d => d.State, op => op.MapFrom(s => s.State))
                .ForMember(d => d.Country, op => op.MapFrom(s => s.Country))
                .ForMember(d => d.PostalCode, op => op.MapFrom(s => s.PostalCode))
                .ForMember(d=>d.UserName, op => op.MapFrom(s => s.user.UserName));

            CreateMap<UpdateAddressDto,Address>()
                .ForMember(d => d.AddressLine1, op => op.MapFrom(s => s.AddressLine1))
                .ForMember(d => d.AddressLine2, op => op.MapFrom(s => s.AddressLine2))
                .ForMember(d => d.City, op => op.MapFrom(s => s.City))
                .ForMember(d => d.State, op => op.MapFrom(s => s.State))
                .ForMember(d => d.Country, op => op.MapFrom(s => s.Country))
                .ForMember(d => d.PostalCode, op => op.MapFrom(s => s.PostalCode));
        }
    }
}
