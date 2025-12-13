using CoreBanking.Domain.Enums;

namespace CoreBanking.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

    
        public Guid DebitAccountId { get; set; }
        public Account DebitAccount { get; set; } = default!;

 
        public Guid CreditAccountId { get; set; }
        public Account CreditAccount { get; set; } = default!;

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Description { get; set; }

        public TransactionType Type { get; set; }
    }
}
