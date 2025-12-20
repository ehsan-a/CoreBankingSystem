using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }

}
