using CoreBanking.Application.CQRS.Commands.Customers;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Customers;
using CoreBanking.Domain.Entities;
namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            var item = await _unitOfWork.Customers.GetByIdAsync(request.Id, spec, cancellationToken);
            if (item == null) return;

            Customer.Delete(item, request.UserId);

            _unitOfWork.Customers.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
