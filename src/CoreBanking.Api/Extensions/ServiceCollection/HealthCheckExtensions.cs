using CoreBanking.Api.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(
            this IServiceCollection services)
        {
            services.AddHealthChecks()
            .AddCheck<CentralBankCreditCheckApiHealthCheck>("CBCCApiHealthCheck", tags: new[] { "centralBankCreditCheckApiHealthCheck" })
            .AddCheck<CivilRegistryApiHealthCheck>("CRApiHealthCheck", tags: new[] { "civilRegistryApiHealthCheck" })
            .AddCheck<PoliceClearanceApiHealthCheck>("PCApiHealthCheck", tags: new[] { "policeClearanceApiHealthCheck" });

            return services;
        }
    }
}
