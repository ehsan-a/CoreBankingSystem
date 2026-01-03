using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var item = await _unitOfWork.Accounts.GetByIdAsync(request.Id, spec, cancellationToken);
            if (item == null) return;

            Account.Delete(item, request.UserId);

            _unitOfWork.Accounts.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
