using CoreBanking.Application.CQRS.Commands.Customers;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.Specifications.Customers;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            var item = await _customerRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            if (item == null) return;

            Customer.Delete(item, request.UserId);

            _customerRepository.Delete(item);
            await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
