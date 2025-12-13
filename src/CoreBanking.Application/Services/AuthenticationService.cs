using CoreBanking.Application.DTOs;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;
using CoreBanking.Application.Specifications.Customers;
using CoreBanking.Domain.Entities;
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
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(ICivilRegistryService civilRegistryService, IPoliceClearanceService policeClearanceService, ICentralBankCreditCheckService centralBankCreditCheckService, IUnitOfWork unitOfWork)
        {
            _civilRegistryService = civilRegistryService;
            _policeClearanceService = policeClearanceService;
            _centralBankCreditCheckService = centralBankCreditCheckService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CivilRegistryResponseDto?> GetCivilRegistryAsync(string nationalCode)
        => await _civilRegistryService.GetPersonInfoAsync(nationalCode);
        public async Task<PoliceClearanceResponseDto?> GetPoliceClearanceAsync(string nationalCode)
        => await _policeClearanceService.GetResultInfoAsync(nationalCode);
        public async Task<CentralBankCreditCheckResponseDto?> GetCentralBankCreditCheckAsync(string nationalCode)
        => await _centralBankCreditCheckService.GetResultInfoAsync(nationalCode);

        public async Task<Authentication> CreateAsync(AuthenticationResponseDto authenticationResponse, CancellationToken cancellationToken)
        {
            var authentication = new Authentication
            {
                NationalCode = authenticationResponse.civilRegistry.NationalCode,
                CreatedAt = DateTime.Now,
                CivilRegistryVerified = authenticationResponse.civilRegistry.IsAlive,
                CentralBankCreditCheckPassed = authenticationResponse.centralBankCreditCheck.IsValid,
                PoliceClearancePassed = authenticationResponse.policeClearance.HasCriminalRecord ? false : true,
            };
            await _unitOfWork.Authentications.AddAsync(authentication, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return authentication;
        }

        public async Task<Authentication?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            var spec = new AuthenticationGetAllSpec();
            return await _unitOfWork.Authentications.GetByNationalCodeAsync(nationalCode, spec, cancellationToken);
        }
        public async Task<bool> ExistsAsync(string nationalCode, CancellationToken cancellationToken)
        {
            var spec = new AuthenticationGetAllSpec();
            return await _unitOfWork.Authentications.ExistsByNationalCodeAsync(nationalCode, spec, cancellationToken);
        }


    }
}
