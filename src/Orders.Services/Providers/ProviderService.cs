using AutoMapper;
using Orders.Repository.Providers;
using Orders.Services.Providers.Dto;

namespace Orders.Services.Providers;

public class ProviderService : IProviderService
{
    private readonly IMapper _mapper;
    private readonly IProviderRepository _providerRepository;

    public ProviderService(IMapper mapper, IProviderRepository providerRepository)
    {
        _mapper = mapper;
        _providerRepository = providerRepository;
    }

    public async Task<IEnumerable<ProviderResponseDto>> GetAsync()
    {
        return _mapper.Map<IEnumerable<ProviderResponseDto>>(
            await _providerRepository.GetAsync());
    }
}
