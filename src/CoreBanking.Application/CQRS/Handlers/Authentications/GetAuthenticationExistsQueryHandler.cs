using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Authentications;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Authentications
{
    public class GetAuthenticationExistsQueryHandler : IQueryHandler<GetAuthenticationExistsQuery, bool>
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public GetAuthenticationExistsQueryHandler(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public async Task<bool> Handle(GetAuthenticationExistsQuery request, CancellationToken cancellationToken)
        {
            return await _authenticationRepository.ExistsByNationalCodeAsync(request.NationalCode, cancellationToken);
        }
    }
}
