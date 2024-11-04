using PaymentRuleService.Dto;

namespace PaymentRuleService;

public class PaymentRuleServices : IPaymentRuleServices
{
    public PaymentProvider GetProviderPaymentOption(OrderCreateRequest orderCreateRequest)
    {
        var totalPrice = orderCreateRequest.Products.Sum(product => product.UnitPrice);
        var (commission, providerName, paymentType) = CalculateCommission(totalPrice);

        if (orderCreateRequest.Method == "Cash")
        {
            return new PaymentProvider { Provider = "PagaFacil", PaymentType = orderCreateRequest.Method };
        }

        if (orderCreateRequest.Method == "Transfer")
        {
            return new PaymentProvider { Provider = "CazaPagos", PaymentType = orderCreateRequest.Method };
        }

        return new PaymentProvider { Provider = providerName, PaymentType = paymentType };
    }

    private (decimal commission, string providerName, string paymentType) CalculateCommission(decimal totalPrice)
    {
        decimal commission = totalPrice * 0.01m;
        string providerName = "PagaFacil";
        string paymentType = "Card";

        if (totalPrice <= 1500)
        {
            decimal commission2 = totalPrice * 0.02m;
            if (commission2 < commission)
            {
                commission = commission2;
                providerName = "CazaPagos";
                paymentType = "CreditCard";
            }
        }
        else if (totalPrice <= 5000)
        {
            decimal commission3 = totalPrice * 0.015m;
            if (commission3 < commission)
            {
                commission = commission3;
                providerName = "CazaPagos";
                paymentType = "CreditCard";
            }
        }
        else
        {
            decimal commission4 = totalPrice * 0.005m;
            if (commission4 < commission)
            {
                commission = commission4;
                providerName = "CazaPagos";
                paymentType = "CreditCard";
            }
        }

        return (commission, providerName, paymentType);
    }
}
