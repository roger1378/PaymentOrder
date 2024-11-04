using PaymentRuleService.Dto;

namespace PaymentRuleService;

public interface IPaymentRuleServices
{
    PaymentProvider GetProviderPaymentOption(OrderCreateRequest orderCreateRequest);
}
