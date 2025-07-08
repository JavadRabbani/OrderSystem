using Application.Orders.Commands;
using Application.Orders.Dtos;
using Application.Orders.Services;
using Application.Orders.Validators;
using Domain.Events;
using Domain.Entities;
using FluentAssertions;
using FluentValidation;
using Infrastructure.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Orders
{
    public class CreateOrderIntegrationTests
    {
        [Fact]
        public async Task CreateOrder_ShouldCreateOrder_AndStoreEvent()
        {
            var command = new CreateOrderCommand()
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<OrderItemDto>()
                {
                    new OrderItemDto(){UnitPrice = 100,ProductId = Guid.NewGuid() ,Quantity = 3},
                    new OrderItemDto(){UnitPrice = 80,ProductId = Guid.NewGuid() ,Quantity = 1}
                }
            };

            Application.Common.Mapping.MapsterConfig.RegisterMappings();

            IValidator<CreateOrderCommand> validator = new CreateOrderValidator();

            var eventStore = new InMemoryEventStore();

            // service
            var service = new OrderApplicationService(validator, eventStore);

            // Act
            var result = await service.CreateOrderAsync(command);

            // Assert
            result.Data.Should().NotBeEmpty();

            var events = eventStore.GetEvents(result.Data);

            events.Should().NotBeEmpty();

            var createdEvent = events.OfType<OrderCreatedEvent>().FirstOrDefault();
            createdEvent.Should().NotBeNull();
            createdEvent!.CustomerId.Should().Be(command.CustomerId);
            createdEvent.ProductIds.Should().BeEquivalentTo(command.Items.Select(i => i.ProductId));
        }
    }
}