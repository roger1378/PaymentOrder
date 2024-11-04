using HttpClientProcess.Order;

namespace Orders.Services.Orders.Dto;

public class OrderResponseDto
{
    public string OrderId { get; set; }

    public string Method { get; set; }

    public int Amount { get; set; }

    public string Status { get; set; }

    public Fee[] Fees { get; set; }

    public Product[] products { get; set; }

    public string ProviderName { get; set; }
}
