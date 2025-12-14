using CoreBanking.Application.Interfaces;
using CoreBanking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.Generators
{
    public class AccountNumberGenerator : INumberGenerator
    {
        private readonly CoreBankingContext _context;

        public AccountNumberGenerator(CoreBankingContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateAccountNumberAsync()
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT NEXT VALUE FOR AccountNumberSequence";

            var result = await command.ExecuteScalarAsync();
            return $"10{Convert.ToInt64(result)}";
        }
    }

}
