using Application.Orders.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Interfaces
{
    public interface IOrderApplicationService
    {
        Task<Guid> CreateOrderAsync(CreateOrderCommand command, CancellationToken ct = default);
    }
}