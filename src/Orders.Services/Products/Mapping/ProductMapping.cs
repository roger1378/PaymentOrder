using AutoMapper;
using Orders.Repository.Models;
using Orders.Services.Products.Dto;

namespace Orders.Services.Products.Mapping;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, ProductResponseDto>()
            .ReverseMap();

        CreateMap<ProductResponseDto, Product>()
            .ReverseMap();

        CreateMap<ProductCreateRequestDto, Product>()
                .ReverseMap();

        CreateMap<ProductUpdateRequestDto, Product>();
    }
}
