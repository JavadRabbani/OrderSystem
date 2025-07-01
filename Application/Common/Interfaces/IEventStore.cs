using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;

namespace Application.Common.Interfaces
{
    public interface IEventStore
    {
        Task SaveAsync(IEvent @event, CancellationToken cancellationToken = default);
    }
}