using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface INumberGenerator
    {
        Task<string> GenerateAccountNumberAsync();
    }
}
