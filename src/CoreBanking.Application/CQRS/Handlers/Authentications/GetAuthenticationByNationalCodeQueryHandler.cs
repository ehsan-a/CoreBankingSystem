using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Authentications;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;

namespace CoreBanking.Application.CQRS.Handlers.Authentications
{
    public class GetAuthenticationByNationalCodeQueryHandler : IQueryHandler<GetAuthenticationByNationalCodeQuery, AuthenticationResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAuthenticationByNationalCodeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponseDto> Handle(GetAuthenticationByNationalCodeQuery request, CancellationToken cancellationToken)
        {
            var spec = new AuthenticationGetAllSpec();
            var authentication = await _unitOfWork.Authentications.GetByNationalCodeAsync(request.NationalCode, spec, cancellationToken);
            return _mapper.Map<AuthenticationResponseDto>(authentication);
        }
    }
}
