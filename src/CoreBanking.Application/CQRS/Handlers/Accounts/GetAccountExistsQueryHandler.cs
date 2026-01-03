using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Accounts;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class GetAccountExistsQueryHandler : IQueryHandler<GetAccountExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(GetAccountExistsQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            return await _unitOfWork.Accounts.ExistsByIdAsync(request.Id, spec, cancellationToken);
        }
    }
}
