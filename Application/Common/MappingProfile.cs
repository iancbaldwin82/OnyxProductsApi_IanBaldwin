using AutoMapper;
using Domain;
using Application.DTOs;

namespace Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDTO, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Product, ProductDTO>();
    }
}