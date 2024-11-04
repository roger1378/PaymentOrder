using HttpClientProcess.Order;

namespace HttpClientProcess;

public interface IPaymentClient
{
    Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest, string path);

    Task<List<OrderResponse>> GetOrderAsync(string path);

    Task<OrderResponse> GetOrderByIdAsync(string path);

    Task CancelOrderAsync(string requestUri, string path);

    Task PayOrderAsync(string requestUri, string path);
}
