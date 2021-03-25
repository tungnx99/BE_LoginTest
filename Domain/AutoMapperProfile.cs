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
            CreateMap<User, UserLogin>().ReverseMap();
            CreateMap<Product, ProductDTOUpadate>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<SerachPaganationDTO<UserDTO>, Paganation<UserDTO>>().ReverseMap();
            CreateMap<SerachPaganationDTO<ProductDTOUpadate>, Paganation<Product>>().ReverseMap();
            //CreateMap(typeof(SerachPaganationDTO<>), typeof(Paganation<>)).ReverseMap();
        }
    }
}
