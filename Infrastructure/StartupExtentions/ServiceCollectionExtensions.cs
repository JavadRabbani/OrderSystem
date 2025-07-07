using Application.Common.Interfaces;
using Infrastructure.EventStore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.StartupExtentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IEventStore, InMemoryEventStore>();

            return services;
        }
    }
}