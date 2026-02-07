using CoreBanking.Api.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CoreBanking.Api.Extensions.ServiceCollection
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddHttpClient(nameof(CentralBankCreditCheckApiHealthCheck), client =>
            {
                client.Timeout = TimeSpan.FromSeconds(5);
            });

            services.AddHttpClient(nameof(CivilRegistryApiHealthCheck), client =>
            {
                client.Timeout = TimeSpan.FromSeconds(5);
            });

            services.AddHttpClient(nameof(PoliceClearanceApiHealthCheck), client =>
            {
                client.Timeout = TimeSpan.FromSeconds(5);
            });

            var conStr = configuration.GetConnectionString("CoreBankingContext")
                ?? throw new InvalidOperationException("Connection string 'CoreBankingContext' not found.");

            services.AddHealthChecks()
            .AddCheck<CentralBankCreditCheckApiHealthCheck>("CBCCApiHealthCheck", tags: new[] { "ready" })
            .AddCheck<CivilRegistryApiHealthCheck>("CRApiHealthCheck", tags: new[] { "ready" })
            .AddCheck<PoliceClearanceApiHealthCheck>("PCApiHealthCheck", tags: new[] { "ready" })
            .AddSqlServer(conStr, tags: new[] { "ready" });

            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                //options.Predicate = healthCheck => healthCheck.Tags.Contains("sample");
            });

            services.AddSingleton<IHealthCheckPublisher, HealthCheckPublisher>();


            return services;
        }
    }
}
