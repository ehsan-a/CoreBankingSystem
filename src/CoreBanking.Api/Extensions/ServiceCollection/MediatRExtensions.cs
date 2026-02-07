using CoreBanking.Application.CQRS.Behaviors;
using System.Reflection;

namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddCustomMediatR(
           this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.Load("CoreBanking.Application"));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }
    }
}