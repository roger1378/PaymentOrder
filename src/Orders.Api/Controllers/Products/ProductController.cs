using Microsoft.AspNetCore.Mvc;
using Orders.Services.Products;
using Orders.Services.Products.Dto;
using System.Net;

namespace Orders.Api.Controllers.Products;

/// <summary>
/// Product controller with authentication.
/// </summary>
[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Returns all products.</summary>
    /// <returns>Returns all products.</returns>
    /// <response code ="200">Returns data for products page.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Products</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _productService.GetAsync();
        return Ok(result);
    }

    /// <summary>Returns product by id.</summary>
    /// <returns>Returns product by id.</returns>
    /// <response code ="200">Returns data for products page.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Products</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>Returns product by ids.</summary>
    /// <returns>Returns product by ids.</returns>
    /// <response code ="200">Returns data for products page.</response>
    /// <response code ="404">Returns Product was not found.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Products</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpGet("ids")]
    public async Task<IActionResult> GetByIdsAsync([FromQuery] List<Guid> ids)
    {
        var result = await _productService.GetByIdsAsync(ids);
        return Ok(result);
    }

    /// <summary>Insert products.</summary>
    /// <returns>Status code.</returns>
    /// <param name="ProductCreateRequestDto">Product information.</param>
    /// <response code ="200">Returns Success.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>Products</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPost()]
    public async Task<IActionResult> InsertAsync(ProductCreateRequestDto productCreateRequestDto)
    {
        var result = await _productService.InsertAsync(productCreateRequestDto);
        return Created("Created", result);
    }

    /// <summary>Update products.</summary>
    /// <returns>Status code.</returns>
    /// <param name="ProductUpdateRequestDto">Product information.</param>
    /// <response code ="200">Returns Success.</response>
    /// <response code ="404">Returns Product was not found.</response>
    /// <response code ="500">Returns an Internal Server Error.</response>
    /// <remarks>WProducts</remarks>
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [HttpPut()]
    public async Task<IActionResult> UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto)
    {
        var result = await _productService.UpdateAsync(productUpdateRequestDto);
        return Ok(result);
    }

    /// <summary>Delete an existing product.</summary>
    /// <param name="id">Product ID.</param>
    /// <response code ="200">Returns OK.</response>
    /// <response code ="404">Returns Product was not found.</response>
    /// <response code ="500">Returns a server error.</response>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTheme([FromRoute] Guid id)
    {
        await _productService.DeleteAsync(id);
        return Ok();
    }
}
