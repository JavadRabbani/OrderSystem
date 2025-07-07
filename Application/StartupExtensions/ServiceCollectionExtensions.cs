using Microsoft.Extensions.DependencyInjection;

namespace Application.StartupExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}