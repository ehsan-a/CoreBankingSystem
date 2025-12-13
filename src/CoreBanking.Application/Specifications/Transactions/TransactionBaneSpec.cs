using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Specifications.Transactions
{
    public abstract class TransactionBaneSpec : Specification<Transaction>
    {
        protected TransactionBaneSpec() { }
    }
}
