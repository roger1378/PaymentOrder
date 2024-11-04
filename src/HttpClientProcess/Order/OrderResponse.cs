namespace HttpClientProcess.Order;

public class OrderResponse
{
    public string orderId { get; set; }
    public decimal amount { get; set; }
    public string status { get; set; }
    public string method { get; set; }
    public Fee[] fees { get; set; }
    public Product[] products { get; set; }
}

public class Fee
{
    public string name { get; set; }
    public decimal amount { get; set; }
}

public class Product
{
    public string name { get; set; }
    public int unitPrice { get; set; }
}