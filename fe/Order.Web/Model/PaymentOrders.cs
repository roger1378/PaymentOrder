using System.Text.Json.Serialization;

namespace Order.Web.Model;

public class PaymentOrder
{
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; }

    [JsonPropertyName("method")]
    public string Method { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("fees")]
    public Fee[] Fees { get; set; }

    [JsonPropertyName("products")]
    public ProductOrder[] Products { get; set; }

    [JsonPropertyName("providerName")]
    public string ProviderName { get; set; }
}
