using FluentValidation;
using Orders.Services.Products.Dto;

namespace Orders.Services.Products.Validators;

public class ProductUpdateRequestDtoValidator : AbstractValidator<ProductUpdateRequestDto>
{
    public ProductUpdateRequestDtoValidator()
    {
        RuleFor(dto => dto).NotNull();

        RuleFor(dto => dto.Id)
            .NotEmpty()
            .NotNull();

        RuleFor(dto => dto.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(128);

        RuleFor(dto => dto.Details)
            .MaximumLength(256);

        RuleFor(dto => dto.UnitPrice)
            .GreaterThan(0);;
    }
}
