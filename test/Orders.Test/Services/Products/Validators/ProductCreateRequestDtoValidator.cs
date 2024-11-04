using FluentAssertions;
using Orders.Services.Products.Dto;
using Orders.Services.Products.Validators;

namespace Orders.Test.Services.Products.Validators;

public class ProductCreateRequestDtoValidatorTest
{
    private readonly ProductCreateRequestDtoValidator _validator;

    public ProductCreateRequestDtoValidatorTest()
    {
        _validator = new ProductCreateRequestDtoValidator();
    }

    [Fact]
    public async Task ValidateAsync_WhenNameIsValid_ShouldReturnIsValid()
    {
        // ARRANGE
        var productCreateRequestDto = new ProductCreateRequestDto
        {
            Name = "My Product",
            UnitPrice = 10.0m,
            Quantity = 10
        };

        // ACT
        var result = await _validator.ValidateAsync(productCreateRequestDto);

        // ASSERT
        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public async Task ValidateAsync_WhenNameIsNullOrEmpty_ShouldReturnNameIsMissing()
    {
        // ARRANGE
        var productCreateRequestDto = new ProductCreateRequestDto
        {
            Name = string.Empty,
            UnitPrice = 10.0m,
            Quantity = 10
        };

        // ACT
        var result = await _validator.ValidateAsync(productCreateRequestDto);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(productCreateRequestDto.Name));
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task ValidateAsync_WhenUnitPriceIsNullOrEmpty_ShouldReturnUnitPriceIsMissing()
    {
        // ARRANGE
        var productCreateRequestDto = new ProductCreateRequestDto
        {
            Name = "My Product",
            UnitPrice = 0,
            Quantity = 10
        };

        // ACT
        var result = await _validator.ValidateAsync(productCreateRequestDto);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(productCreateRequestDto.UnitPrice));
        result.Errors.Should().HaveCount(1);
    }
}
