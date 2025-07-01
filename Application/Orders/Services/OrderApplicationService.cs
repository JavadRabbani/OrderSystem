using Application.Orders.Commands;
using Domain.Entities;
using Domain.Entities.Domain.Entities;
using FluentValidation;
using Application.Common.Interfaces;
using Application.Orders.Interfaces;
using Mapster;

namespace Application.Orders.Services
{
    public class OrderApplicationService : IOrderApplicationService
    {
        private readonly IValidator<CreateOrderCommand> _validator;

        private readonly IEventStore _eventStore;

        public OrderApplicationService(
            IValidator<CreateOrderCommand> validator,
            IEventStore eventStore)
        {
            _validator = validator;
            _eventStore = eventStore;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderCommand command, CancellationToken ct = default)
        {
            var validation = await _validator.ValidateAsync(command, ct);

            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);
            try
            {
                var items = command.Items.Adapt<List<OrderItem>>(); // از Mapster استفاده کردیم

                var (order, @event) = Order.Create(command.CustomerId, items);

                await _eventStore.SaveAsync(@event, ct);

                return order.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}