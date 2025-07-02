using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Orders.Commands;
using Application.Orders.Dtos;
using Application.Orders.Services;
using Domain.Events;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Unit.Application.Orders.Services
{
    [Collection("SharedTestCollection")]
    public class OrderApplicationServiceTests
    {
        [Fact]
        public async Task CreateOrderAsync_ShouldCreateOrder_WhenInputIsValid()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<OrderItemDto>
                {new() { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 }
                    ,
                    new() { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 200 }
                }
            };

            var validatorMock = new Mock<IValidator<CreateOrderCommand>>();
            validatorMock
                .Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult()); // معتبر فرض می‌کنیم

            var eventStoreMock = new Mock<IEventStore>();

            Guid savedOrderId = Guid.Empty;

            eventStoreMock
                .Setup(s => s.SaveAsync(It.IsAny<IEvent>(), It.IsAny<CancellationToken>()))
                .Callback<IEvent, CancellationToken>((ev, ct) =>
                {
                    if (ev is OrderCreatedEvent oc)
                        savedOrderId = oc.AggregateId;
                })
                .Returns(Task.CompletedTask);

            var service = new OrderApplicationService(validatorMock.Object, eventStoreMock.Object);

            // Act
            var result = await service.CreateOrderAsync(command);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Be(savedOrderId);

            validatorMock.Verify(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()), Times.Once);
            eventStoreMock.Verify(s => s.SaveAsync(It.IsAny<IEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldThrowValidationException_WhenInputIsInvalid()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerId = Guid.Empty, // نامعتبر
                Items = new List<OrderItemDto>() // خالی
            };

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("CustomerId", "CustomerId is required"),
                new ValidationFailure("Items", "At least one item is required")
            };

            var validatorMock = new Mock<IValidator<CreateOrderCommand>>();
            validatorMock
                .Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(validationErrors));

            var eventStoreMock = new Mock<IEventStore>();

            var service = new OrderApplicationService(validatorMock.Object, eventStoreMock.Object);

            // Act
            Func<Task> act = () => service.CreateOrderAsync(command);

            // Assert
            await act.Should()
                .ThrowAsync<ValidationException>()
                .WithMessage("*CustomerId is required*");

            eventStoreMock.Verify(
                s => s.SaveAsync(It.IsAny<IEvent>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}