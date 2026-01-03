using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Customers;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using System.Text.Json;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
            if (customer == null) throw new NotFoundException("");

            _mapper.Map(request, customer);

            var oldAccount = await _unitOfWork.Customers
              .GetByIdAsNoTrackingAsync(customer.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            _unitOfWork.Customers.Update(customer);

            Customer.Update(customer, request.UserId, oldValue);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
