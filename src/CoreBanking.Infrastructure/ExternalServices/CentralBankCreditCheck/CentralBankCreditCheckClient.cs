using CoreBanking.Application.DTOs;
using CoreBanking.Application.Interfaces;
using CoreBanking.Infrastructure.ExternalServices.CivilRegistry;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace CoreBanking.Infrastructure.ExternalServices.CentralBankCreditCheck
{
    public class CentralBankCreditCheckClient : ICentralBankCreditCheckService
    {
        private readonly HttpClient _httpClient;

        public CentralBankCreditCheckClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CentralBankCreditCheckResponseDto?> GetResultInfoAsync(string nationalCode)
        {
            var response = await _httpClient.GetAsync($"api/centralbankcreditcheck/{nationalCode}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var reslut = await _httpClient.GetFromJsonAsync<CentralBankCreditCheckDto>($"api/centralbankcreditcheck/{nationalCode}");
            return new CentralBankCreditCheckResponseDto
            {
                IsValid = reslut.IsValid,
                Reason = reslut.Reason,
                CheckedAt = reslut.CheckedAt,
            };
        }
    }
}
