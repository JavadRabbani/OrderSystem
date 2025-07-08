using Application.Orders.Interfaces;
using Application.Orders.Services;
using Application.Orders.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Middlewares;

namespace Application.StartupExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateOrderValidator).Assembly);
            services.AddScoped<IOrderApplicationService, OrderApplicationService>();
            return services;
        }
    }
}