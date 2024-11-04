namespace PaymentRuleService.Dto;

public class OrderCreateRequest
{
    public string Method { get; set; }

    public IEnumerable<OrderProductsRule> Products { get; set; }
}
