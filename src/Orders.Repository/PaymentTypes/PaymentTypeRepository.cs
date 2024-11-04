using Microsoft.EntityFrameworkCore;
using Orders.Repository.Models;

namespace Orders.Repository.PaymentTypes;

public class PaymentTypeRepository : IPaymentTypeRepository
{
    private readonly PaymentOrdersDbContext _paymentOrdersDbContext;

    public PaymentTypeRepository(PaymentOrdersDbContext paymentOrdersDbContext)
    {
        _paymentOrdersDbContext = paymentOrdersDbContext;
    }

    public async Task<IEnumerable<PaymentType>> GetAsync()
    {
        return await _paymentOrdersDbContext.PaymentTypes
            .ToListAsync();
    }
}
