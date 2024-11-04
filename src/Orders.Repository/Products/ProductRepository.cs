using Microsoft.EntityFrameworkCore;
using Orders.Repository.Models;

namespace Orders.Repository.Products;

public class ProductRepository : IProductRepository
{
    private readonly PaymentOrdersDbContext _paymentOrdersDbContext;

    public ProductRepository(PaymentOrdersDbContext paymentOrdersDbContext)
    {
        _paymentOrdersDbContext = paymentOrdersDbContext;
    }

    public async Task<IEnumerable<Product>> GetAsync()
    {
        return await _paymentOrdersDbContext.Products
            .Where(product => product.IsActive)
            .ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _paymentOrdersDbContext.Products
            .Where(product => product.Id == id && product.IsActive)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetByIdsAsync(List<Guid> ids)
    {
        return await _paymentOrdersDbContext.Products
            .Where(product => ids.Contains(product.Id) && product.IsActive)
            .ToListAsync();
    }

    public async Task<Product> InsertAsync(Product product)
    {
        _paymentOrdersDbContext.Products.Add(product);
        await _paymentOrdersDbContext.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _paymentOrdersDbContext.Update(product).State = EntityState.Modified;
        await _paymentOrdersDbContext.SaveChangesAsync();

        return product;
    }

    public async Task<int> DeleteAsync(Product product)
    {
        _paymentOrdersDbContext.Entry(product).State = EntityState.Modified;
        return await _paymentOrdersDbContext.SaveChangesAsync();
    }
}
