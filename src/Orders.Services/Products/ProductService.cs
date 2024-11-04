using AutoMapper;
using FluentValidation;
using Orders.Repository.Models;
using Orders.Repository.Products;
using Orders.Services.Products.Dto;

namespace Orders.Services.Products;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IValidator<ProductCreateRequestDto> _productCreateRequestDtoValidator;
    private readonly IValidator<ProductUpdateRequestDto> _productUpdateRequestDtoValidator;

    public ProductService(
        IMapper mapper,
        IProductRepository productRepository,
        IValidator<ProductCreateRequestDto> productCreateRequestDtoValidator,
        IValidator<ProductUpdateRequestDto> productUpdateRequestDtoValidator)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _productCreateRequestDtoValidator = productCreateRequestDtoValidator;
        _productUpdateRequestDtoValidator = productUpdateRequestDtoValidator;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAsync()
    {
        return _mapper.Map<IEnumerable<ProductResponseDto>>(
            await _productRepository.GetAsync());
    }

    public async Task<ProductResponseDto> GetByIdAsync(Guid id)
    {
        await AssertProductExists(id);

        return _mapper.Map<ProductResponseDto>(
            await _productRepository.GetByIdAsync(id));
    }

    public async Task<IEnumerable<ProductResponseDto>> GetByIdsAsync(List<Guid> ids)
    {
        foreach (var id in ids)
        {
            await AssertProductExists(id);
        }

        return _mapper.Map<IEnumerable<ProductResponseDto>>(
            await _productRepository.GetByIdsAsync(ids));
    }

    public async Task<ProductResponseDto> InsertAsync(ProductCreateRequestDto productCreateRequestDto)
    {
        await _productCreateRequestDtoValidator.ValidateAndThrowAsync(productCreateRequestDto);

        var product = _mapper.Map<Product>(productCreateRequestDto);
        product.Id = Guid.NewGuid();
        product.IsActive = true;

        var newProduct = await _productRepository.InsertAsync(product);

        return _mapper.Map<ProductResponseDto>(newProduct);
    }

    public async Task<ProductResponseDto> UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto)
    {
        await _productUpdateRequestDtoValidator.ValidateAndThrowAsync(productUpdateRequestDto);

        var product = await AssertProductExists(productUpdateRequestDto.Id);

        var modelProduct = _mapper.Map(productUpdateRequestDto, product);
        await _productRepository.UpdateAsync(modelProduct);

        return _mapper.Map<ProductResponseDto>(modelProduct);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await AssertProductExists(id);
        product.IsActive = false;

        await _productRepository.DeleteAsync(product);
    }

    private async Task<Product> AssertProductExists(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"The Product with Id {id} could not be found.");
        }

        return product;
    }
}
