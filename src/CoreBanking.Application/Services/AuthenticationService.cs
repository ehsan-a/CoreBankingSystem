using CoreBanking.Application.DTOs;
using CoreBanking.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreBanking.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICivilRegistryService _civilRegistryService;
        private readonly IPoliceClearanceService _policeClearanceService;
        private readonly ICentralBankCreditCheckService _centralBankCreditCheckService;

        public AuthenticationService(ICivilRegistryService civilRegistryService, IPoliceClearanceService policeClearanceService, ICentralBankCreditCheckService centralBankCreditCheckService)
        {
            _civilRegistryService = civilRegistryService;
            _policeClearanceService = policeClearanceService;
            _centralBankCreditCheckService = centralBankCreditCheckService;
        }

        public async Task<CivilRegistryResponseDto?> GetCivilRegistryAsync(string nationalCode)
        => await _civilRegistryService.GetPersonInfoAsync(nationalCode);
        public async Task<PoliceClearanceResponseDto?> GetPoliceClearanceAsync(string nationalCode)
        => await _policeClearanceService.GetResultInfoAsync(nationalCode);
        public async Task<CentralBankCreditCheckResponseDto?> GetCentralBankCreditCheckAsync(string nationalCode)
        => await _centralBankCreditCheckService.GetResultInfoAsync(nationalCode);


    }
}
