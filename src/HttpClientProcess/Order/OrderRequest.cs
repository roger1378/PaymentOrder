namespace HttpClientProcess.Order;

public class OrderRequest
{
    public string Method { get; set; }

    public IEnumerable<OrderProducts> Products { get; set; }
}
