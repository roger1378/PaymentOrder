using AutoMapper;
using FluentValidation;
using HttpClientProcess;
using HttpClientProcess.Order;
using Orders.Repository.Models;
using Orders.Repository.Orders;
using Orders.Services.Orders.Dto;
using Orders.Services.Orders.Enum;
using Orders.Services.Providers;
using PaymentRuleService;
using PaymentRuleService.Dto;

namespace Orders.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<OrderCreateRequestDto> _orderCreateRequestDtoValidator;
    private readonly IPaymentClient _paymentClient;
    private readonly IPaymentRuleServices _paymentRuleServices;
    private readonly IProviderService _providerService;

    public OrderService(
        IMapper mapper,
        IOrderRepository orderRepository,
        IValidator<OrderCreateRequestDto> orderCreateRequestDtoValidator,
        IPaymentClient paymentClient,
        IPaymentRuleServices paymentRuleServices,
        IProviderService providerService)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _orderCreateRequestDtoValidator = orderCreateRequestDtoValidator;
        _paymentClient = paymentClient;
        _paymentRuleServices = paymentRuleServices;
        _providerService = providerService;
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAsync()
    {
        var resultPagaFacil = await _paymentClient.GetOrderAsync("https://app-paga-chg-aviva.azurewebsites.net/Order");
        var resultCazaPagos = await _paymentClient.GetOrderAsync("https://app-caza-chg-aviva.azurewebsites.net/Order");

        var result = resultPagaFacil.Concat(resultCazaPagos).ToList();

        var orderResponse = _mapper.Map<List<OrderResponseDto>>(result);
        var orders = await _orderRepository.GetDetaisAsync();

        orderResponse.ForEach(order =>
        {
            var customOrder = orders.FirstOrDefault(o => o.ReferenceId == order.OrderId);
            if (customOrder != null)
            {
                order.ProviderName = customOrder.Provider.Name;
            }
        });

        return orderResponse;
    }

    public async Task<OrderResponseDto> GetByIdAsync(string id)
    {
        var order = await AssertOrderExists(id);

        var ordenResponse = await _paymentClient.GetOrderByIdAsync(
            string.Concat(GetProviderUrl(order.Provider.Name), $"Order/{id}"));
        
        var orderResponse = _mapper.Map<OrderResponseDto>(ordenResponse);
        
        orderResponse.ProviderName = order.Provider.Name;
        
        return orderResponse;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateRequestDto orderCreateRequestDto)
    {
        await _orderCreateRequestDtoValidator.ValidateAndThrowAsync(orderCreateRequestDto);

        var orderCreateRequest = _mapper.Map<OrderCreateRequest>(orderCreateRequestDto);
        var paymentProvider = _paymentRuleServices.GetProviderPaymentOption(orderCreateRequest);

        orderCreateRequestDto.Method = paymentProvider.PaymentType;
        var newOrder = _mapper.Map<OrderRequest>(orderCreateRequestDto);

        var result = await _paymentClient.CreateOrderAsync(newOrder,
            GetProviderUrl(paymentProvider.Provider));

        var orderResponse = _mapper.Map<OrderResponseDto>(result);
        orderResponse.ProviderName = paymentProvider.Provider;

        var providers = await _providerService.GetAsync();

        var providerId = providers.FirstOrDefault(p => p.Name == paymentProvider.Provider)?.Id;
        if (providerId == null)
        {
            throw new KeyNotFoundException($"The provider with name {paymentProvider.Provider} could not be found.");
        }

        var order = new Order();
        order.Id = Guid.NewGuid();
        order.ProviderId = Guid.Parse(providerId.ToString());
        order.Status = orderResponse.Status;
        order.ReferenceId = orderResponse.OrderId;

        await _orderRepository.InsertAsync(order);

        return orderResponse;
    }

    public async Task CancelAsync(string id)
    {
        var order = await AssertOrderExists(id.ToString());
        if (order.Status == OrderStatus.Cancelled.ToString())
        {
            throw new InvalidOperationException($"The Order with Id {id} is already cancel.");
        }

        var requestUri = $"Cancel?id={id}";
        if (order.Provider.Name == "CazaPagos" || order.Provider.Name == "Transfer")
        {
            requestUri = $"cancellation?id={id}";
        }

        await _paymentClient.CancelOrderAsync(requestUri, GetProviderUrl(order.Provider.Name));

        order.Status = OrderStatus.Cancelled.ToString();
        await _orderRepository.UpdateAsync(order);
    }

    public async Task PayAsync(string id)
    {
        var order = await AssertOrderExists(id.ToString());
        if(order.Status == OrderStatus.Paid.ToString())
        {
            throw new InvalidOperationException($"The Order with Id {id} is already paid.");
        }
        if (order.Status == OrderStatus.Cancelled.ToString())
        {
            throw new InvalidOperationException($"The Order with Id {id} was cancelled.");
        }

        var requestUri = $"pay?id={id}";
        if (order.Provider.Name == "CazaPagos")
        {
            requestUri = $"payment?id={id}";
        }

        await _paymentClient.PayOrderAsync(requestUri, GetProviderUrl(order.Provider.Name));

        order.Status = OrderStatus.Paid.ToString();
        await _orderRepository.UpdateAsync(order);
    }

    private async Task<Order> AssertOrderExists(string id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            throw new KeyNotFoundException($"The Order with Id {id} could not be found.");
        }

        return order;
    }

    private string GetProviderUrl(string providerName)
    {
        string url = string.Empty;

        if (providerName == "PagaFacil")
        {
            url = "https://app-paga-chg-aviva.azurewebsites.net/";
        }
        if (providerName == "CazaPagos" || providerName == "Transfer")
        {
            url = "https://app-caza-chg-aviva.azurewebsites.net/";
        }

        return url;
    }
}
