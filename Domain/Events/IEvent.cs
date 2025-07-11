﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public interface IEvent
    {
        Guid AggregateId { get; }

        DateTime OccurredAt { get; }
    }
}