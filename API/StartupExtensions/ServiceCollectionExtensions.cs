using Application.Common.Mapping;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection ConfigureMapster(this IServiceCollection services)
        {
            MapsterConfig.RegisterMappings();
            return services;
        }
    }
}