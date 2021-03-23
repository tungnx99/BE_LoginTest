using AutoMapper;
using Common.Paganation;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<SerachPaganationDTO<UserDTO>, Paganation<UserDTO>>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<SerachPaganationDTO<ProductDTO>, Paganation<Domain.Entities.Product>>().ReverseMap();
        }
    }
}
