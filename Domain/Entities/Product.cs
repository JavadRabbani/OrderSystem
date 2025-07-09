using Domain.Enums;
using Domain.Events;

namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;

    namespace Domain.Entities
    {
        public class Product
        {
            public Guid Id { get; set; }

            public string Title { get; set; }

            public int RamInGB { get; set; }

            public int StorageInGB { get; set; }

            public int CpuCores { get; set; }

            public PeriodType PeriodType { get; set; } // Enum

            public int Duration { get; set; } // مثلاً 6 ماهه، یا 30 روزه

            public DateTime StartDate { get; set; }

            public DateTime EndDate => CalculateEndDate();

            private DateTime CalculateEndDate()
            {
                return PeriodType switch
                {
                    PeriodType.Daily => StartDate.AddDays(Duration),
                    PeriodType.Monthly => StartDate.AddMonths(Duration),
                    PeriodType.Yearly => StartDate.AddYears(Duration),
                    _ => throw new NotSupportedException("Period type not supported")
                };
            }
        }
    }
}