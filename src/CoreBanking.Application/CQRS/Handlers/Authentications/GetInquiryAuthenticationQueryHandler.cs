using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Authentications;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.DTOs.Responses.ExternalServices;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Authentications
{
    public class GetInquiryAuthenticationQueryHandler : IQueryHandler<GetInquiryAuthenticationQuery, AuthenticationResponseDto>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;
        private readonly ICivilRegistryService _civilRegistryService;
        private readonly IPoliceClearanceService _policeClearanceService;
        private readonly ICentralBankCreditCheckService _centralBankCreditCheckService;

        public GetInquiryAuthenticationQueryHandler(IAuthenticationRepository authenticationRepository, IMapper mapper, ICivilRegistryService civilRegistryService, IPoliceClearanceService policeClearanceService, ICentralBankCreditCheckService centralBankCreditCheckService)
        {
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
            _civilRegistryService = civilRegistryService;
            _policeClearanceService = policeClearanceService;
            _centralBankCreditCheckService = centralBankCreditCheckService;
        }

        public async Task<AuthenticationResponseDto> Handle(GetInquiryAuthenticationQuery request, CancellationToken cancellationToken)
        {
            var person = await GetCivilRegistryAsync(request.NationalCode);
            var spec = new AuthenticationGetAllSpec();
            var authentication = await _authenticationRepository.GetByNationalCodeAsync(request.NationalCode, spec, cancellationToken);
            if (person == null) throw new NotFoundException("Person", request.NationalCode);
            return new AuthenticationResponseDto
            {
                CivilRegistry = await GetCivilRegistryAsync(request.NationalCode),
                CentralBankCreditCheck = await GetCentralBankCreditCheckAsync(request.NationalCode),
                PoliceClearance = await GetPoliceClearanceAsync(request.NationalCode),
                RegisteredAuthentication = _mapper.Map<RegisteredAuthResponseDto>(authentication)
            };
        }

        public async Task<CivilRegistryResponseDto?> GetCivilRegistryAsync(string nationalCode)
        => await _civilRegistryService.GetPersonInfoAsync(nationalCode);
        public async Task<PoliceClearanceResponseDto?> GetPoliceClearanceAsync(string nationalCode)
        => await _policeClearanceService.GetResultInfoAsync(nationalCode);
        public async Task<CentralBankCreditCheckResponseDto?> GetCentralBankCreditCheckAsync(string nationalCode)
        => await _centralBankCreditCheckService.GetResultInfoAsync(nationalCode);
    }
}
