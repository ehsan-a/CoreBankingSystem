using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Specifications.Accounts
{
    public class AccountGetAllSpec : AccountBaseSpec
    {
        public AccountGetAllSpec(int? limit = null, int? offset = null)
        {
            if (limit is not null && offset is not null)
            {
                ApplyPaging(offset.Value, limit.Value);
            }
        }
    }
}
