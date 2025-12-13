using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Specifications.Accounts
{
    public abstract class AccountBaseSpec : Specification<Account>
    {
        protected AccountBaseSpec() { }
    }
}
