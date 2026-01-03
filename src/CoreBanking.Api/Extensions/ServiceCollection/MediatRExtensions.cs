using System.Reflection;

namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddCustomMediatR(
           this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("CoreBanking.Application")));

            return services;
        }
    }
}
