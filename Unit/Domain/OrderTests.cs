using Domain.Entities.Domain.Entities;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Threading.Channels;

namespace Unit.Domain
{
    public class OrderTests
    {
        [Fact]
        public void Create_ShouldReturnOrderAndOrderCreatedEvent()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var items = new List<OrderItem>
            {
                new(Guid.NewGuid(), 2, 10.5m),
                new(Guid.NewGuid(), 1, 99.99m)
            };

            // Act
            var (order, @event) = Order.Create(customerId, items);

            // Assert
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(customerId);
            order.Status.Should().Be(OrderStatus.Pending);
            order.Items.Should().HaveCount(2);

            @event.Should().NotBeNull();
            @event.CustomerId.Should().Be(customerId);
            @event.ProductIds.Should().BeEquivalentTo(items.ConvertAll(i => i.ProductId));
        }

        [Fact]
        public void Confirm_ShouldChangeStatusToConfirmed_WhenPending()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var items = new List<OrderItem>
            {
                new(Guid.NewGuid(), 2, 10.5m),
                new(Guid.NewGuid(), 1, 99.99m)
            };

            // Act
            var (order, @event) = Order.Create(customerId, items);

            // Assert
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(customerId);
            order.Status.Should().Be(OrderStatus.Pending);
            order.Items.Should().HaveCount(2);

            @event.Should().NotBeNull();
            @event.CustomerId.Should().Be(customerId);
            @event.ProductIds.Should().BeEquivalentTo(items.ConvertAll(i => i.ProductId));



            order.Confirm();

            order.Status.Should().Be(OrderStatus.Confirmed);
        }

        [Fact]
        public void Confirm_ShouldThrow_WhenAlreadyConfirmed()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var items = new List<OrderItem>
            {
                new(Guid.NewGuid(), 2, 10.5m),
                new(Guid.NewGuid(), 1, 99.99m)
            };

            // Act
            var (order, @event) = Order.Create(customerId, items);

            // Assert
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(customerId);
            order.Status.Should().Be(OrderStatus.Pending);
            order.Items.Should().HaveCount(2);

            @event.Should().NotBeNull();
            @event.CustomerId.Should().Be(customerId);
            @event.ProductIds.Should().BeEquivalentTo(items.ConvertAll(i => i.ProductId));



            order.Confirm();

            order.Status.Should().Be(OrderStatus.Confirmed);


            Action act = () => order.Confirm();
            act.Should().Throw<InvalidOperationException>().WithMessage("Order is not in a confirmable state.");
        }
        [Fact]
        public void Cancel_ShouldThrow_WhenAlreadyConfirmed()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var items = new List<OrderItem>
            {
                new(Guid.NewGuid(), 2, 10.5m),
                new(Guid.NewGuid(), 1, 99.99m)
            };

            // Act
            var (order, @event) = Order.Create(customerId, items);

            // Assert
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(customerId);
            order.Status.Should().Be(OrderStatus.Pending);
            order.Items.Should().HaveCount(2);

            @event.Should().NotBeNull();
            @event.CustomerId.Should().Be(customerId);
            @event.ProductIds.Should().BeEquivalentTo(items.ConvertAll(i => i.ProductId));



            order.Confirm();

            order.Status.Should().Be(OrderStatus.Confirmed);


            Action actCancel = () => order.Cancel();
            actCancel.Should().Throw<InvalidOperationException>().WithMessage("Cannot cancel a confirmed order.");


        }
        [Fact]
        public void Confirm_ShouldThrow_WhenAlreadyCanceled()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var items = new List<OrderItem>
            {
                new(Guid.NewGuid(), 2, 10.5m),
                new(Guid.NewGuid(), 1, 99.99m)
            };

            // Act
            var (order, @event) = Order.Create(customerId, items);

            // Assert
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(customerId);
            order.Status.Should().Be(OrderStatus.Pending);
            order.Items.Should().HaveCount(2);

            @event.Should().NotBeNull();
            @event.CustomerId.Should().Be(customerId);
            @event.ProductIds.Should().BeEquivalentTo(items.ConvertAll(i => i.ProductId));



            order.Cancel();

            order.Status.Should().Be(OrderStatus.Cancelled);


            Action actCancel = () => order.Confirm();
            actCancel.Should().Throw<InvalidOperationException>().WithMessage("Order is not in a confirmable state.");


        }
    }
}
