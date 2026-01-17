using CoreBanking.Domain.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Net;

namespace CoreBanking.Api.HealthChecks
{
    public class CivilRegistryApiHealthCheck : IHealthCheck
    {
        private readonly BaseUrlConfiguration _baseUrlConfiguration;

        public CivilRegistryApiHealthCheck(IOptions<BaseUrlConfiguration> baseUrlConfiguration)
        {
            _baseUrlConfiguration = baseUrlConfiguration.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string myUrl = _baseUrlConfiguration.CivilRegistryBaseAddress + "api/civilregistry/";
            var client = new HttpClient();
            var response = await client.GetAsync(myUrl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return HealthCheckResult.Healthy("The check indicates a healthy result.");
            }

            return HealthCheckResult.Unhealthy("The check indicates an unhealthy result.");
        }
    }
}
