using AutoMapper;
using Orders.Repository.Models;
using Orders.Services.Providers.Dto;

namespace Orders.Services.Products.Mapping;

public class ProviderMapping : Profile
{
    public ProviderMapping()
    {
        CreateMap<Provider, ProviderResponseDto>()
            .ReverseMap();
    }
}
