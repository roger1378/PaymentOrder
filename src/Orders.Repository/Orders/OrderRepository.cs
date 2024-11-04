using Microsoft.EntityFrameworkCore;
using Orders.Repository.Models;

namespace Orders.Repository.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly PaymentOrdersDbContext _paymentOrdersDbContext;

    public OrderRepository(PaymentOrdersDbContext paymentOrdersDbContext)
    {
        _paymentOrdersDbContext = paymentOrdersDbContext;
    }

    public async Task<IEnumerable<Order>> GetAsync()
    {
        return await _paymentOrdersDbContext.Orders
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetDetaisAsync()
    {
        return await _paymentOrdersDbContext.Orders
            .Include(order => order.Provider)
            .ToListAsync();
    }

    public async Task<Order> GetByIdAsync(string id)
    {
        return await _paymentOrdersDbContext.Orders
            .Include(order => order.Provider)
            .Where(order => order.ReferenceId == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<Order> InsertAsync(Order order)
    {
        _paymentOrdersDbContext.Orders.Add(order);
        await _paymentOrdersDbContext.SaveChangesAsync();

        return order;
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        _paymentOrdersDbContext.Update(order).State = EntityState.Modified;
        await _paymentOrdersDbContext.SaveChangesAsync();

        return order;
    }
}
