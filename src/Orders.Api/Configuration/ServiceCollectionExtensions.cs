using FluentValidation;
using HttpClientProcess;
using Orders.Repository.Orders;
using Orders.Repository.Products;
using Orders.Repository.Providers;
using Orders.Services.Orders;
using Orders.Services.Orders.Validators;
using Orders.Services.Products;
using Orders.Services.Products.Validators;
using Orders.Services.Providers;
using PaymentRuleService;

namespace Orders.Api.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMappers(
        this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddValidatorsFromAssemblyContaining<ProductCreateRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<ProductUpdateRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<OrderCreateRequestDtoValidator>();

        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(
         this IServiceCollection services)
    {
        //Repositories
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProviderRepository, ProviderRepository>();

        //Services
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProviderService, ProviderService>();

        services.AddSingleton<IPaymentClient, PaymentClient>();
        services.AddSingleton<IPaymentRuleServices, PaymentRuleServices>();

        return services;
    }
}
