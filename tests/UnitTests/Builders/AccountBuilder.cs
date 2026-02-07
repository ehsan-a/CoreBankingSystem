using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Builders
{
    public class AccountBuilder
    {
        private Account _account;

        public string AccountNumber => "101000000001";
        public Guid CustomerId => new Guid("3692D21F-3B31-4B41-A5D4-2A9AF937147E");
        public Guid UserId => new Guid("4692D21F-3B31-4B41-A5D4-2A9AF937147E");

        public AccountBuilder()
        {
            _account = WithDefaultValue();
        }

        public Account Build()
        {
            return _account;
        }

        public Account WithDefaultValue()
        {
            return _account = Account.Create(AccountNumber, CustomerId, UserId);
        }
    }
}
