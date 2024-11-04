using AutoMapper;
using HttpClientProcess.Order;
using Orders.Repository.Models;
using Orders.Services.Orders.Dto;
using PaymentRuleService.Dto;

namespace Orders.Services.Orders.Mapping;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<Order, OrderResponseDto>()
            .ReverseMap();

        CreateMap<OrderCreateRequestDto, OrderRequest>()
            .ForPath(dest => dest.Method, opt => opt.MapFrom(src => src.Method))
            .ForPath(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<OrderResponse, OrderResponseDto>()
            .ReverseMap();

        CreateMap<OrderProductsDto, OrderProducts>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));

        CreateMap<OrderCreateRequestDto, OrderCreateRequest>()
            .ForPath(dest => dest.Method, opt => opt.MapFrom(src => src.Method))
            .ForPath(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<OrderProductsDto, OrderProductsRule>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
    }
}
