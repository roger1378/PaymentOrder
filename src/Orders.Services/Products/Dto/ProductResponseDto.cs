namespace Orders.Services.Products.Dto;

public class ProductResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal UnitPrice { get; set; }

    public string Details { get; set; }

    public string Status { get; set; }
}
