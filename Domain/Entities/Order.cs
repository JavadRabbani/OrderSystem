using Domain.Enums;

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

            public Order(Guid customerId, List<OrderItem> items)
            {
                Id = Guid.NewGuid();
                CustomerId = customerId;
                CreatedAt = DateTime.UtcNow;
                Items = items ?? new List<OrderItem>();
                Status = OrderStatus.Pending;
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
        }
    }
}