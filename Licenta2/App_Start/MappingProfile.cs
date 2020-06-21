using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Licenta2.Dtos;
using Licenta2.Models;

namespace Licenta2.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Equipment, EquipmentDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Category, CategoryDto>();

            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<EquipmentDto, Equipment>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}