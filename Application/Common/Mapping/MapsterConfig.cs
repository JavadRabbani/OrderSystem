using Application.Orders.Dtos;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<OrderItemDto, OrderItem>.NewConfig()
                .ConstructUsing(src => new OrderItem(src.ProductId, src.Quantity, src.UnitPrice));
        }
    }
}