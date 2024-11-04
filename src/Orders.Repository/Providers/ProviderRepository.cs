using Microsoft.EntityFrameworkCore;
using Orders.Repository.Models;

namespace Orders.Repository.Providers;

public class ProviderRepository : IProviderRepository
{
    private readonly PaymentOrdersDbContext _paymentOrdersDbContext;

    public ProviderRepository(PaymentOrdersDbContext paymentOrdersDbContext)
    {
        _paymentOrdersDbContext = paymentOrdersDbContext;
    }

    public async Task<IEnumerable<Provider>> GetAsync()
    {
        return await _paymentOrdersDbContext.Providers
            .Where(provider => provider.IsActive)
            .ToListAsync();
    }
}
