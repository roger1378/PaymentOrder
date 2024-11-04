using Orders.Repository.Models;

namespace Orders.Repository.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAsync();

    Task<Product> GetByIdAsync(Guid id);

    Task<IEnumerable<Product>> GetByIdsAsync(List<Guid> ids);

    Task<Product> InsertAsync(Product product);

    Task<Product> UpdateAsync(Product product);

    Task<int> DeleteAsync(Product product);
}
