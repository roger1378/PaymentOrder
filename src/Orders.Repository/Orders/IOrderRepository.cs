using Orders.Repository.Models;

namespace Orders.Repository.Orders;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAsync();

    Task<IEnumerable<Order>> GetDetaisAsync();

    Task<Order> GetByIdAsync(string id);

    Task<Order> InsertAsync(Order order);

    Task<Order> UpdateAsync(Order order);
}
