using Orders.Services.Orders.Dto;

namespace Orders.Services.Orders;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseDto>> GetAsync();

    Task<OrderResponseDto> GetByIdAsync(string id);

    Task<OrderResponseDto> CreateOrderAsync(OrderCreateRequestDto orderCreateRequestDto);

    Task CancelAsync(string id);

    Task PayAsync(string id);
}
