using Azure;
using CoreBanking.Application.DTOs;
using CoreBanking.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoreBanking.Infrastructure.ExternalServices.CivilRegistry
{
    public class CivilRegistryClient : ICivilRegistryService
    {
        private readonly HttpClient _httpClient;

        public CivilRegistryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CivilRegistryResponseDto?> GetPersonInfoAsync(string nationalCode)
        {
            var response = await _httpClient.GetAsync($"api/civilregistry/{nationalCode}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var person = await _httpClient.GetFromJsonAsync<CivilRegistryDto>($"api/civilregistry/{nationalCode}");
            return new CivilRegistryResponseDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                NationalCode = person.NationalCode,
                IsAlive = person.IsAlive,
            };
        }
    }
}
