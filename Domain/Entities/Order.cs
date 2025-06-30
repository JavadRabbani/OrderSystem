using Domain.Enums;
using Domain.Events;

namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;

    namespace Domain.Entities
    {
        public class Order
        {
            public Guid Id { get; private set; }

            public Guid CustomerId { get; private set; }

            public OrderStatus Status { get; private set; }

            public DateTime CreatedAt { get; private set; }

            public List<OrderItem> Items { get; private set; }
            private Order() { }

            public Order(OrderCreatedEvent e)
            {
                Id = e.AggregateId;
                CustomerId = e.CustomerId;
                CreatedAt = e.OccurredAt;
                Status = OrderStatus.Pending;
                Items = e.ProductIds.Select(id => new OrderItem(id, 1, 0)).ToList(); // placeholder
            }
            public void Confirm()
            {
                if (Status != OrderStatus.Pending)
                    throw new InvalidOperationException("Order is not in a confirmable state.");

                Status = OrderStatus.Confirmed;
            }

            public void Cancel()
            {
                if (Status == OrderStatus.Confirmed)
                    throw new InvalidOperationException("Cannot cancel a confirmed order.");

                Status = OrderStatus.Cancelled;
            }
            public static (Order order, OrderCreatedEvent @event) Create(
                Guid customerId,
                List<OrderItem> items)
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    CreatedAt = DateTime.UtcNow,
                    Items = items ?? new(),
                    Status = OrderStatus.Pending
                };

                var productIds = items.Select(i => i.ProductId).ToList();

                var @event = new OrderCreatedEvent(
                    order.Id,
                    customerId,
                    productIds
                );

                return (order, @event);
            }

        }
    }
}