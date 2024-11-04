using Order.Web.Model;
using System.Text;
using System.Text.Json;

namespace BlazorProducts.Web.Services;

public class ApiService
{
    public HttpClient _httpClient;

    public ApiService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var response = await _httpClient.GetAsync("product");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Product>>(jsonString);
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"product/{id}");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Product>(jsonString);
    }

    public async Task<List<Product>> GetProductsByIdsAsync(List<string> ids)
    {
        var queryString = string.Join("&", ids.Select(id => $"ids={id}"));
        var response = await _httpClient.GetAsync($"Product/ids?{queryString}");

        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Product>>(jsonString);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        var json = JsonSerializer.Serialize(product);
        var stringContent = new StringContent(json,
            UnicodeEncoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("product", stringContent);

        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Product>(jsonString);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var json = JsonSerializer.Serialize(product);
        var stringContent = new StringContent(json,
            UnicodeEncoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync("product", stringContent);

        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Product>(jsonString);
    }

    public async Task DeleteProductAsync(string id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"product/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<PaymentOrder>> GetPaymentOrdersAsync()
    {
        var response = await _httpClient.GetAsync("order");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<PaymentOrder>>(jsonString);
    }

    public async Task<PaymentOrder> CreateOrderAsync(OrderCreateRequest orderCreateRequest)
    {
        var json = JsonSerializer.Serialize(orderCreateRequest);
        var stringContent = new StringContent(json,
            UnicodeEncoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("order", stringContent);

        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PaymentOrder>(jsonString);
    }

    public async Task CancelOrderAsync(string id)
    {
        HttpResponseMessage response = await _httpClient.PutAsync($"order/cancel?id={id}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task PayOrderAsync(string id)
    {
        HttpResponseMessage response = await _httpClient.PutAsync($"order/pay?id={id}", null);
        response.EnsureSuccessStatusCode();
    }
}