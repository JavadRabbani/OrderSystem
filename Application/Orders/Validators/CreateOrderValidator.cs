using Application.Orders.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one order item is required.");

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .NotEmpty().WithMessage("ProductId is required.");

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

                items.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("UnitPrice must be greater than 0.");
            });
        }
    }
}