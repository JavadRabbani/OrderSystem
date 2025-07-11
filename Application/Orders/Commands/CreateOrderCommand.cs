﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Orders.Dtos;

namespace Application.Orders.Commands
{
    public class CreateOrderCommand
    {
        public Guid CustomerId { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();
    }
}