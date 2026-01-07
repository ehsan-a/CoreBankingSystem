using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Accounts;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class GetAccountDebitTransactionsQueryHandler : IQueryHandler<GetAccountDebitTransactionsQuery, IEnumerable<TransactionResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountDebitTransactionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionResponseDto>> Handle(GetAccountDebitTransactionsQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var account = await _unitOfWork.Accounts.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<IEnumerable<TransactionResponseDto>>(account.DebitTransactions);
        }
    }
}
