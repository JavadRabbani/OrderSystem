using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Orders.Commands;
using Application.Orders.Dtos;
using Application.Orders.Validators;
using FluentValidation.TestHelper;

namespace Unit.Application.Orders.Validators
{
    public class CreateOrderValidatorTests
    {
        private readonly CreateOrderValidator _validator = new();

        [Fact]
        public void Should_HaveError_When_CustomerId_IsEmpty()
        {
            var command = new CreateOrderCommand
            {
                CustomerId = Guid.Empty,
                Items = new List<OrderItemDto>
                {
                    new() { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 }
                }
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.CustomerId);
        }

        [Fact]
        public void Should_HaveError_When_Items_AreMissing()
        {
            var command = new CreateOrderCommand
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<OrderItemDto>()
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Items);
        }

        [Fact]
        public void Should_Not_HaveError_For_Valid_Command()
        {
            var command = new CreateOrderCommand
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<OrderItemDto>
                {
                    new() { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 50 }
                }
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}