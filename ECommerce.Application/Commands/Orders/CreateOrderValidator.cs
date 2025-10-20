using FluentValidation;

namespace ECommerce.Application.Commands.Orders
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("CustomerId is required.");

            RuleFor(x => x.Products)
                .NotEmpty()
                .WithMessage("Order must contain at least one product.");

            RuleForEach(x => x.Products).ChildRules(product =>
            {
                product.RuleFor(p => p.ProductId)
                    .GreaterThan(0)
                    .WithMessage("ProductId must be valid.");

                product.RuleFor(p => p.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than zero.");
            });
        }
    }
}
