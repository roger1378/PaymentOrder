using Microsoft.AspNetCore.Mvc;
using Moq;
using Orders.Api.Controllers.Products;
using Orders.Services.Products;
using Orders.Services.Products.Dto;
using Orders.Services.Products.Enum;

namespace Orders.Test.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductController(_mockProductService.Object);
    }

    [Fact]
    public async Task GetAsync_ReturnsOkResult_WithListOfProducts()
    {
        // Arrange
        var products = new List<ProductResponseDto>
        {
            new ProductResponseDto { Id = Guid.NewGuid(), Name = "Product1", UnitPrice = 10, Details = "Details1", Status = "Available" },
            new ProductResponseDto { Id = Guid.NewGuid(), Name = "Product2", UnitPrice = 20, Details = "Details2", Status = "NotAvailable" }
        };
        _mockProductService.Setup(service => service.GetAsync()).ReturnsAsync(products);

        // Act
        var result = await _controller.GetAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ProductResponseDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task InsertAsync_ReturnsCreatedResult_WithProduct()
    {
        // Arrange
        var productCreateRequest = new ProductCreateRequestDto { Name = "Product1", UnitPrice = 10, Details = "Details1", Status = "Available", Quantity = 5 };
        var productResponse = new ProductResponseDto { Id = Guid.NewGuid(), Name = "Product1", UnitPrice = 10, Details = "Details1", Status = "Available" };
        _mockProductService.Setup(service => service.InsertAsync(productCreateRequest)).ReturnsAsync(productResponse);

        // Act
        var result = await _controller.InsertAsync(productCreateRequest);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        var returnValue = Assert.IsType<ProductResponseDto>(createdResult.Value);
        Assert.Equal(productCreateRequest.Name, returnValue.Name);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsOkResult_WithUpdatedProduct()
    {
        // Arrange
        var productUpdateRequest = new ProductUpdateRequestDto { Id = Guid.NewGuid(), Name = "UpdatedProduct", UnitPrice = 15, Details = "UpdatedDetails", Status = "Available", Quantity = 10 };
        var productResponse = new ProductResponseDto { Id = productUpdateRequest.Id, Name = "UpdatedProduct", UnitPrice = 15, Details = "UpdatedDetails", Status = "Available" };
        _mockProductService.Setup(service => service.UpdateAsync(productUpdateRequest)).ReturnsAsync(productResponse);

        // Act
        var result = await _controller.UpdateAsync(productUpdateRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ProductResponseDto>(okResult.Value);
        Assert.Equal(productUpdateRequest.Name, returnValue.Name);
    }

    [Fact]
    public async Task DeleteTheme_ReturnsOkResult()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _mockProductService.Setup(service => service.DeleteAsync(productId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteTheme(productId);

        // Assert
        Assert.IsType<OkResult>(result);
    }
}
