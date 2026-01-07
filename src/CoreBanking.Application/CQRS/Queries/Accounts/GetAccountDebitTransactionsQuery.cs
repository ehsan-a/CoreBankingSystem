using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.CQRS.Queries.Accounts
{
    public class GetAccountDebitTransactionsQuery : IQuery<IEnumerable<TransactionResponseDto>>
    {
        public Guid Id { get; set; }
    }
}
