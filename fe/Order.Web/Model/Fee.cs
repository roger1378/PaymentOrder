using System.Text.Json.Serialization;

namespace Order.Web.Model;

public class Fee
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}
