using CoreBanking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Requests.Transaction
{
    public class CreateTransactionRequestDto
    {
        public Guid DebitAccountId { get; set; }
        public Guid CreditAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
    }
}
