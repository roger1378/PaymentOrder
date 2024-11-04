using FluentValidation;
using Orders.Services.Orders.Dto;

namespace Orders.Services.Orders.Validators;

public class OrderCreateRequestDtoValidator : AbstractValidator<OrderCreateRequestDto>
{
    public OrderCreateRequestDtoValidator()
    {
        RuleFor(dto => dto).NotNull();

        RuleFor(dto => dto.Method)
            .NotEmpty()
            .NotNull()
            .MaximumLength(32);

        RuleFor(dto => dto.Products)
            .NotNull()
            .NotEmpty();
    }
}
