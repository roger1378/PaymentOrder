using Orders.Repository.Models;

namespace Orders.Repository.PaymentTypes;

public interface IPaymentTypeRepository
{
    Task<IEnumerable<PaymentType>> GetAsync();
}
