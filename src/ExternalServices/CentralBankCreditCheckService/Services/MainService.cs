using CentralBankCreditCheckService.Data;
using CentralBankCreditCheckService.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CentralBankCreditCheckService.Services
{
    public class MainService
    {
        private readonly CentralBankCreditCheckDbContext _context;

        public MainService(CentralBankCreditCheckDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult?> GetResultAsync(string nationalCode)
        => await _context.ValidationResults.FirstOrDefaultAsync(x => x.NationalCode == nationalCode);

    }
}
