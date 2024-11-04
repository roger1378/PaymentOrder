using Orders.Repository.Models;

namespace Orders.Repository.Providers;

public interface IProviderRepository
{
    Task<IEnumerable<Provider>> GetAsync();
}
