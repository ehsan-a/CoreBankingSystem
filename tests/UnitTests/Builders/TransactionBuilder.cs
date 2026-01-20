using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;

namespace UnitTests.Builders
{
    public class TransactionBuilder
    {
        private Transaction _transaction;

        public Guid DebitAccountId => new Guid("3692D21F-3B31-4B41-A5D4-2A9AF937147E");
        public Guid CreditAccountId => new Guid("4692D21F-3B31-4B41-A5D4-2A9AF937147E");
        public decimal Amount => 100;
        public string Description => "Unit Testing";
        public TransactionType Type => TransactionType.Transfer;

        public TransactionBuilder()
        {
            _transaction = WithDefaultValue();
        }

        public Transaction Build()
        {
            return _transaction;
        }

        public Transaction WithDefaultValue()
        {
            return _transaction = new Transaction(DebitAccountId, CreditAccountId, Amount, Description, Type);
        }
    }
}
