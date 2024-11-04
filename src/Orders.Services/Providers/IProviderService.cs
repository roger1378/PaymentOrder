using Orders.Services.Providers.Dto;

namespace Orders.Services.Providers;

public interface IProviderService
{
    Task<IEnumerable<ProviderResponseDto>> GetAsync();
}
