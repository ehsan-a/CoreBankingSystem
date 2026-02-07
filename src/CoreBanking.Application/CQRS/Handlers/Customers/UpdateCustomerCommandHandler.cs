using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Customers;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using System.Text.Json;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
            if (customer == null) throw new NotFoundException("");

            _mapper.Map(request, customer);

            var oldAccount = await _customerRepository
              .GetByIdAsNoTrackingAsync(customer.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            _customerRepository.Update(customer);

            Customer.Update(customer, request.UserId, oldValue);

            await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
