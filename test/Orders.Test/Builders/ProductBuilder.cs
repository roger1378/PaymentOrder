using Orders.Repository.Models;

namespace Orders.Test.Builders;

public class ProductBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _name = "Test";
    private decimal _unitPrice = 10.0m;
    private string _details = "Test";
    private int _quantity = 10;
    private string _status = "Available";
    private bool _isActive = true;

    private ProductBuilder()
    {

    }

    public static ProductBuilder Builder()
    {
        return new ProductBuilder();
    }

    public Product Build()
    {
        return new Product
        {
            Id = _id,
            Name = _name,
            UnitPrice = _unitPrice,
            Details = _details,
            Quantity = _quantity,
            Status = _status,
            IsActive = _isActive
        };
    }

    internal ProductBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    internal ProductBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    internal ProductBuilder WithUnitPrice(decimal unitPrice)
    {
        _unitPrice = unitPrice;
        return this;
    }

    internal ProductBuilder WithDetails(string details)
    {
        _details = details;
        return this;
    }

    internal ProductBuilder WithQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }

    internal ProductBuilder WithStatus(string status)
    {
        _status = status;
        return this;
    }

    internal ProductBuilder WithIsActive(bool isActive)
    {
        _isActive = isActive;
        return this;
    }
}
