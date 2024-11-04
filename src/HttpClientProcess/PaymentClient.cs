using HttpClientProcess.Order;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HttpClientProcess;

public class PaymentClient : IPaymentClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PaymentClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest, string path)
    {
        using var httpClient = _httpClientFactory.CreateClient("Payment");
        InitializeClientAsync(httpClient, path);

        var orderResponse = new OrderResponse();

        var json = JsonSerializer.Serialize(orderRequest);
        var stringContent = new StringContent(json, 
            UnicodeEncoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync(
            "Order", stringContent);
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            orderResponse = JsonSerializer.Deserialize<OrderResponse>(jsonString);
        }
        return orderResponse;
    }

    public async Task<List<OrderResponse>> GetOrderAsync(string path)
    {
        using var httpClient = _httpClientFactory.CreateClient("Payment");
        InitializeClientAsync(httpClient, path);

        var orderResponse = new List<OrderResponse>();
        HttpResponseMessage response = await httpClient.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            if (!jsonString.Contains("[]"))
            {
                orderResponse = JsonSerializer.Deserialize<List<OrderResponse>>(jsonString);
            }
        }
        return orderResponse;
    }

    public async Task<OrderResponse> GetOrderByIdAsync(string path)
    {
        using var httpClient = _httpClientFactory.CreateClient("Payment");
        InitializeClientAsync(httpClient, path);

        var orderResponse = new OrderResponse();
        HttpResponseMessage response = await httpClient.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            if (!jsonString.Contains("[]"))
            {
                orderResponse = JsonSerializer.Deserialize<OrderResponse>(jsonString);
            }
        }
        return orderResponse;
    }

    public async Task CancelOrderAsync(string requestUri, string path)
    {
        using var httpClient = _httpClientFactory.CreateClient("Payment");
        InitializeClientAsync(httpClient, path);

        HttpResponseMessage response = await httpClient.PutAsync(requestUri, null);
        response.EnsureSuccessStatusCode();
    }

    public async Task PayOrderAsync(string requestUri, string path)
    {
        using var httpClient = _httpClientFactory.CreateClient("Payment");
        InitializeClientAsync(httpClient, path);

        HttpResponseMessage response = await httpClient.PutAsync(requestUri, null);
        response.EnsureSuccessStatusCode();
    }

    private void InitializeClientAsync(HttpClient httpClient, string url)
    {
        httpClient.BaseAddress = new Uri(url);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Add("x-api-key", "apikey-fj9esodija09s2");
    }
}
