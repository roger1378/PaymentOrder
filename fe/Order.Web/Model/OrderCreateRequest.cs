namespace Order.Web.Model;

public class OrderCreateRequest
{
    public string Method { get; set; }

    public IEnumerable<OrderProductsRequest> Products { get; set; }
}

public class OrderProductsRequest
{
    public string Name { get; set; }

    public decimal UnitPrice { get; set; }
}
