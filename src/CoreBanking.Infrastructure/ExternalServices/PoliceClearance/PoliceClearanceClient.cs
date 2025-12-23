using CoreBanking.Application.DTOs.Responses.ExternalServices;
using CoreBanking.Application.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace CoreBanking.Infrastructure.ExternalServices.PoliceClearance
{
    public class PoliceClearanceClient : IPoliceClearanceService
    {
        private readonly HttpClient _httpClient;

        public PoliceClearanceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PoliceClearanceResponseDto?> GetResultInfoAsync(string nationalCode)
        {
            var response = await _httpClient.GetAsync($"api/policeclearance/{nationalCode}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var reslut = await _httpClient.GetFromJsonAsync<PoliceClearanceDto>($"api/policeclearance/{nationalCode}");
            return new PoliceClearanceResponseDto
            {
                HasCriminalRecord = reslut.HasCriminalRecord,
                Description = reslut.Description,
                CheckedAt = reslut.CheckedAt,
            };
        }
    }
}
