using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using System.Text.Json;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Accounts.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
            if (account == null) throw new NotFoundException("");

            _mapper.Map(request, account);

            var oldAccount = await _unitOfWork.Accounts
              .GetByIdAsNoTrackingAsync(account.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            _unitOfWork.Accounts.Update(account);

            Account.Update(account, request.UserId, oldValue);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
