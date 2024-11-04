using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Orders.Repository.Models;
using Orders.Repository.Products;
using Orders.Services.Products;
using Orders.Services.Products.Dto;
using Orders.Services.Products.Enum;
using Orders.Services.Products.Mapping;
using Orders.Test.Builders;

namespace Orders.Test.Services.Products;

public class ProductServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IValidator<ProductCreateRequestDto>> _productCreateRequestDtoValidatorMock;
    private readonly Mock<IValidator<ProductUpdateRequestDto>> _productUpdateRequestDtoValidatorMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProductMapping());
        });
        _mapper = mapperConfiguration.CreateMapper();

        _productRepositoryMock = new Mock<IProductRepository>();
        _productCreateRequestDtoValidatorMock = new Mock<IValidator<ProductCreateRequestDto>>();
        _productUpdateRequestDtoValidatorMock = new Mock<IValidator<ProductUpdateRequestDto>>();

        _productService = new ProductService(
            _mapper,
            _productRepositoryMock.Object,
            _productCreateRequestDtoValidatorMock.Object,
            _productUpdateRequestDtoValidatorMock.Object);
    }

    [Fact]
    public async Task GetAsync_WhenRequested_ShouldReturnAllActiveProducts()
    {
        var product = ProductBuilder.Builder()
            .WithName("Test Product")
            .Build();

        var expectedProducts = new List<Product> { product };

        _productRepositoryMock
            .Setup(repository => repository.GetAsync())
            .ReturnsAsync(expectedProducts);

        // ACT
        var activeProducts = await _productService.GetAsync();

        // ASSERT
        Assert.NotNull(activeProducts);
        Assert.Single(activeProducts);
        Assert.Equal("Test Product", activeProducts.First().Name);
    }

    [Fact]
    public async Task InsertAsync_WhenRequested_ShouldInsertProduct()
    {
        // Management
        var productCreateRequestDto = new ProductCreateRequestDto()
        {
            Name = "Test Product",
            Details = "Details",
            UnitPrice = 10.0m,
            Quantity = 10,
            Status = "Available"
        };

        var product = ProductBuilder.Builder()
            .WithName("Test Product")
            .Build();

        _productRepositoryMock
            .Setup(repository => repository.InsertAsync(It.IsAny<Product>()))
            .ReturnsAsync(product);

        var result = await _productService.InsertAsync(productCreateRequestDto);

        // ASSERT
        Assert.NotNull(result);
        result.Name.Should().Be(productCreateRequestDto.Name);
        result.Status.Should().Be("Available");
    }

    [Fact]
    public async Task UpdateAsync_WhenRequested_ShouldUpdateProduct()
    {
        var product = ProductBuilder.Builder()
            .WithName("Test Product")
            .Build();

        _productRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        _productRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Product>()))
            .ReturnsAsync(product);

        var productUpdateRequestDto = new ProductUpdateRequestDto()
        {
            Id = product.Id,
            Name = "Test Product",
            Details = "Details",
            UnitPrice = 10.0m,
            Quantity = 10,
            Status = "Available"
        };

        var result = await _productService.UpdateAsync(productUpdateRequestDto);

        // ASSERT
        Assert.NotNull(result);
        result.Name.Should().Be(productUpdateRequestDto.Name);
        result.Status.Should().Be("Available");
    }

    [Fact]
    public async Task DeleteAsync_WhenRequested_ShouldDeleteProduct()
    {
        // ARRANGE
        var product = ProductBuilder.Builder()
            .Build();

        _productRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        _productRepositoryMock
            .Setup(r => r.DeleteAsync(It.IsAny<Product>()))
            .ReturnsAsync(1);

        // ACT
        await _productService.DeleteAsync(product.Id);

        // ASSERT
        _productRepositoryMock.Verify(r => r.DeleteAsync(product));
    }
}
