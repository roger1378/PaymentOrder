using Orders.Services.Products.Enum;

namespace Orders.Services.Products.Dto;

public class ProductUpdateRequestDto
{
    public  Guid Id { get; set; }

    public string Name { get; set; }

    public decimal UnitPrice { get; set; }

    public string Details { get; set; }

    public string Status { get; set; }

    public int Quantity { get; set; }
}
