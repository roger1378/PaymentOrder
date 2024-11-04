using Orders.Services.Orders.Enum;

namespace Orders.Services.Orders.Dto;

public class OrderCreateRequestDto
{
    public string Method { get; set; }

    public IEnumerable<OrderProductsDto> Products { get; set; }
}
