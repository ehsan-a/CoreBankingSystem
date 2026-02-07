using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Transactions;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Transactions
{
    public class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, TransactionResponseDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IIdempotencyService _idempotencyService;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IMapper mapper, IIdempotencyService idempotencyService)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
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

            await _transactionRepository.UnitOfWork.BeginTransactionAsync();

            try
            {
                var spec = new AccountGetAllSpec();
                var from = await _accountRepository.GetByIdAsync(request.DebitAccountId, spec, cancellationToken);
                var to = await _accountRepository.GetByIdAsync(request.CreditAccountId, spec, cancellationToken);

                from.Debit(request.Amount);
                to.Credit(request.Amount);

                var transaction = _mapper.Map<Transaction>(request);
                await _transactionRepository.AddAsync(transaction, cancellationToken);
                Transaction.Create(transaction, request.IdempotencyKey, request.UserId);

                await _transactionRepository.UnitOfWork.CommitTransactionAsync(_transactionRepository.UnitOfWork.GetCurrentTransaction());
                return _mapper.Map<TransactionResponseDto>(transaction);
            }
            catch
            {
                _transactionRepository.UnitOfWork.RollbackTransaction();
                throw;
            }

        }
    }
}
