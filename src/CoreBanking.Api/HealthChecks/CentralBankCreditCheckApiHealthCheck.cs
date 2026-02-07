using CoreBanking.Domain.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace CoreBanking.Api.HealthChecks
{
    public class CentralBankCreditCheckApiHealthCheck : IHealthCheck
    {
        private readonly BaseUrlConfiguration _baseUrlConfiguration;
        private readonly HttpClient _httpClient;

        public CentralBankCreditCheckApiHealthCheck(
            IOptions<BaseUrlConfiguration> baseUrlConfiguration,
            IHttpClientFactory httpClientFactory)
        {
            _baseUrlConfiguration = baseUrlConfiguration.Value;
            _httpClient = httpClientFactory.CreateClient(nameof(CentralBankCreditCheckApiHealthCheck));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var url = $"{_baseUrlConfiguration.CentralBankCreditCheckBaseAddress}api/centralbankcreditcheck/";

                using var response = await _httpClient.GetAsync(url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("Central Bank Credit Check API is reachable.");
                }

                return HealthCheckResult.Unhealthy(
                    "Central Bank API returned non-success status code",
                    data: new Dictionary<string, object>
                    {
                        ["StatusCode"] = response.StatusCode
                    });
            }
            catch (TaskCanceledException ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Central Bank API request timed out.",
                    ex);
            }
            catch (HttpRequestException ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Cannot reach Central Bank API.",
                    ex);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Unexpected error while checking Central Bank API health.",
                    ex);
            }
        }
    }
}
