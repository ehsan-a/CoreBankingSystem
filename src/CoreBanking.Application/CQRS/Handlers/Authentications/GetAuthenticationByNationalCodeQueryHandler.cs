using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Authentications;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.Specifications.Authentications;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Authentications
{
    public class GetAuthenticationByNationalCodeQueryHandler : IQueryHandler<GetAuthenticationByNationalCodeQuery, AuthenticationResponseDto>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;

        public GetAuthenticationByNationalCodeQueryHandler(IAuthenticationRepository authenticationRepository, IMapper mapper)
        {
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponseDto> Handle(GetAuthenticationByNationalCodeQuery request, CancellationToken cancellationToken)
        {
            var spec = new AuthenticationGetAllSpec();
            var authentication = await _authenticationRepository.GetByNationalCodeAsync(request.NationalCode, spec, cancellationToken);
            return _mapper.Map<AuthenticationResponseDto>(authentication);
        }
    }
}
