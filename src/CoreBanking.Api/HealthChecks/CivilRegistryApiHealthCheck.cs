using CoreBanking.Domain.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace CoreBanking.Api.HealthChecks
{
    public class CivilRegistryApiHealthCheck : IHealthCheck
    {
        private readonly BaseUrlConfiguration _baseUrlConfiguration;
        private readonly HttpClient _httpClient;

        public CivilRegistryApiHealthCheck(
            IOptions<BaseUrlConfiguration> baseUrlConfiguration,
            IHttpClientFactory httpClientFactory)
        {
            _baseUrlConfiguration = baseUrlConfiguration.Value;
            _httpClient = httpClientFactory.CreateClient(nameof(CivilRegistryApiHealthCheck));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var url = $"{_baseUrlConfiguration.CivilRegistryBaseAddress}api/civilregistry/";

                using var response = await _httpClient.GetAsync(url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("Civil Registry API is reachable.");
                }

                return HealthCheckResult.Unhealthy(
                    "Civil Registry API returned non-success status code.",
                    data: new Dictionary<string, object>
                    {
                        ["StatusCode"] = (int)response.StatusCode
                    });
            }
            catch (TaskCanceledException ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Civil Registry API request timed out.",
                    ex);
            }
            catch (HttpRequestException ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Cannot reach Civil Registry API.",
                    ex);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Unexpected error while checking Civil Registry API health.",
                    ex);
            }
        }
    }
}
