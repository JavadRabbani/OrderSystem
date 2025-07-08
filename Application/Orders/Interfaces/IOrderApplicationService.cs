using Application.Orders.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Dto;

namespace Application.Orders.Interfaces
{
    public interface IOrderApplicationService
    {
        Task<ApiResult<Guid>> CreateOrderAsync(CreateOrderCommand command, CancellationToken ct = default);
    }
}