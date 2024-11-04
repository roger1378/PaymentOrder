using System.Text.Json.Serialization;

namespace Order.Web.Model;

public class ProductOrder
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("unitPrice")]
    public int UnitPrice { get; set; }
}
