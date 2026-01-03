using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Authentications;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;

namespace CoreBanking.Application.CQRS.Handlers.Authentications
{
    public class GetAuthenticationExistsQueryHandler : IQueryHandler<GetAuthenticationExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthenticationExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(GetAuthenticationExistsQuery request, CancellationToken cancellationToken)
        {
            var spec = new AuthenticationGetAllSpec();
            return await _unitOfWork.Authentications.ExistsByNationalCodeAsync(request.NationalCode, spec, cancellationToken);
        }
    }
}
