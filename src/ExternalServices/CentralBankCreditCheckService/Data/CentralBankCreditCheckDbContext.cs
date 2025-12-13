using CentralBankCreditCheckService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CentralBankCreditCheckService.Data
{
    public class CentralBankCreditCheckDbContext : DbContext
    {
        public CentralBankCreditCheckDbContext(DbContextOptions<CentralBankCreditCheckDbContext> options) : base(options)
        {
        }

        protected CentralBankCreditCheckDbContext()
        {
        }
        public DbSet<ValidationResult> ValidationResults => Set<ValidationResult>();
    }
}
