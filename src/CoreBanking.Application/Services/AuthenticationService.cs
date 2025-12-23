using AutoMapper;
using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.DTOs.Responses.ExternalServices;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;
using CoreBanking.Domain.Entities;
using FluentValidation;

namespace CoreBanking.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICivilRegistryService _civilRegistryService;
        private readonly IPoliceClearanceService _policeClearanceService;
        private readonly ICentralBankCreditCheckService _centralBankCreditCheckService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAuthenticationRequestDto> _validator;

        public AuthenticationService(ICivilRegistryService civilRegistryService, IPoliceClearanceService policeClearanceService, ICentralBankCreditCheckService centralBankCreditCheckService, IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAuthenticationRequestDto> validator)
        {
            _civilRegistryService = civilRegistryService;
            _policeClearanceService = policeClearanceService;
            _centralBankCreditCheckService = centralBankCreditCheckService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CivilRegistryResponseDto?> GetCivilRegistryAsync(string nationalCode)
        => await _civilRegistryService.GetPersonInfoAsync(nationalCode);
        public async Task<PoliceClearanceResponseDto?> GetPoliceClearanceAsync(string nationalCode)
        => await _policeClearanceService.GetResultInfoAsync(nationalCode);
        public async Task<CentralBankCreditCheckResponseDto?> GetCentralBankCreditCheckAsync(string nationalCode)
        => await _centralBankCreditCheckService.GetResultInfoAsync(nationalCode);

        public async Task<Authentication> CreateAsync(CreateAuthenticationRequestDto createAuthenticationRequestDto, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(createAuthenticationRequestDto);
            var authenticationExists = await _unitOfWork.Authentications.ExistsByNationalCodeAsync(createAuthenticationRequestDto.NationalCode, cancellationToken);

            if (authenticationExists)
            {
                throw new ConflictException("Authentication already exists.");
            }
            var authentication = _mapper.Map<Authentication>(createAuthenticationRequestDto);
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
        public async Task<AuthenticationResponseDto> GetInquiryAsync(string nationalCode, CancellationToken cancellationToken)
        {
            var person = await GetCivilRegistryAsync(nationalCode);
            if (person == null) throw new NotFoundException("Person", nationalCode);
            return new AuthenticationResponseDto
            {
                civilRegistry = await GetCivilRegistryAsync(nationalCode),
                centralBankCreditCheck = await GetCentralBankCreditCheckAsync(nationalCode),
                policeClearance = await GetPoliceClearanceAsync(nationalCode),
                RegisteredAuthentication = _mapper.Map<RegisteredAuthResponseDto>(await GetByNationalCodeAsync(nationalCode, cancellationToken))
            };
        }
    }
}
