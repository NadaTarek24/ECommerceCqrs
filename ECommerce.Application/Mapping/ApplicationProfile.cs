using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Mapping;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer!.Name))
            .ForMember(dest => dest.NumberOfProducts, opt => opt.MapFrom(src => src.OrderItems.Count));
    }
}
