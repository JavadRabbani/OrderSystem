using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class OrderCreatedEvent : IEvent
    {
        public Guid AggregateId { get; }

        public Guid CustomerId { get; }

        public List<Guid> ProductIds { get; }

        public DateTime OccurredAt { get; }

        public OrderCreatedEvent(Guid orderId, Guid customerId, List<Guid> productIds)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            ProductIds = productIds;
            OccurredAt = DateTime.UtcNow;
        }
    }
}