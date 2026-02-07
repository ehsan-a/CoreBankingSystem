using CoreBanking.Domain.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace CoreBanking.Api.HealthChecks
{
    public class PoliceClearanceApiHealthCheck : IHealthCheck
    {
        private readonly BaseUrlConfiguration _baseUrlConfiguration;
        private readonly HttpClient _httpClient;

        public PoliceClearanceApiHealthCheck(
            IOptions<BaseUrlConfiguration> baseUrlConfiguration,
            IHttpClientFactory httpClientFactory)
        {
            _baseUrlConfiguration = baseUrlConfiguration.Value;
            _httpClient = httpClientFactory.CreateClient(nameof(PoliceClearanceApiHealthCheck));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var url = $"{_baseUrlConfiguration.PoliceClearanceBaseAddress}api/policeclearance/";

                using var response = await _httpClient.GetAsync(url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("Police Clearance API is reachable.");
                }

                return HealthCheckResult.Unhealthy(
                    "Police Clearance API returned non-success status code.",
                    data: new Dictionary<string, object>
                    {
                        ["StatusCode"] = (int)response.StatusCode
                    });
            }
            catch (TaskCanceledException ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Police Clearance API request timed out.",
                    ex);
            }
            catch (HttpRequestException ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Cannot reach Police Clearance API.",
                    ex);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Unexpected error while checking Police Clearance API health.",
                    ex);
            }
        }
    }
}
