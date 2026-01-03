using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Customers;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Customers;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class GetCustomerExistsQueryHandler : IQueryHandler<GetCustomerExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(GetCustomerExistsQuery request, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            return await _unitOfWork.Customers.ExistsByIdAsync(request.Id, spec, cancellationToken);
        }
    }
}
