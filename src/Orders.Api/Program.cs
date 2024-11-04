using Microsoft.EntityFrameworkCore;
using Orders.Api.Configuration;
using Orders.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add configuration for connection string
var connectionString = builder.Configuration.GetConnectionString("OrdersDB");
builder.Services.AddDbContext<PaymentOrdersDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services
    .AddMappers()
    .AddMyDependencyGroup();

builder.Services.AddHttpClient("Payment", (httpClient) => {});

////builder.Services.AddHttpClient("CazaPagos", (httpClient) =>
////{
////    httpClient.BaseAddress = new Uri("https://app-caza-chg-aviva.azurewebsites.net/");
////});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
