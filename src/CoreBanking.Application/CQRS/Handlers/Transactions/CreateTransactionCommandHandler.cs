using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Transactions;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.CQRS.Handlers.Transactions
{
    public class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, TransactionResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IIdempotencyService _idempotencyService;

        public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdempotencyService idempotencyService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _idempotencyService = idempotencyService;
        }

        public async Task<TransactionResponseDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {

            if (request.DebitAccountId == request.CreditAccountId)
                throw new ConflictException("The parties to the transaction are the same.");

            var previousResult = await _idempotencyService.GetResultAsync(request.IdempotencyKey, request.UserId);
            if (previousResult != null)
            {
                throw new ConflictException(previousResult);
            }

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var spec = new AccountGetAllSpec();
                var from = await _unitOfWork.Accounts.GetByIdAsync(request.DebitAccountId, spec, cancellationToken);
                var to = await _unitOfWork.Accounts.GetByIdAsync(request.CreditAccountId, spec, cancellationToken);

                from.Debit(request.Amount);
                to.Credit(request.Amount);

                var transaction = _mapper.Map<Transaction>(request);
                await _unitOfWork.Transactions.AddAsync(transaction, cancellationToken);
                Transaction.Create(transaction, request.IdempotencyKey, request.UserId);

                await _unitOfWork.CommitAsync();
                return _mapper.Map<TransactionResponseDto>(transaction);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }
    }
}
