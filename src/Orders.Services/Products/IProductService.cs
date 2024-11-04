using Orders.Services.Products.Dto;

namespace Orders.Services.Products;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAsync();

    Task<ProductResponseDto> GetByIdAsync(Guid id);

    Task<IEnumerable<ProductResponseDto>> GetByIdsAsync(List<Guid> ids);

    Task<ProductResponseDto> InsertAsync(ProductCreateRequestDto productCreateRequestDto);

    Task<ProductResponseDto> UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto);

    Task DeleteAsync(Guid id);
}
