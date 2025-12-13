using CentralBankCreditCheckService.Models;
using System;

namespace CentralBankCreditCheckService.Data
{
    public static class SeedData
    {
        public static void Seed(CentralBankCreditCheckDbContext db)
        {
            if (db.ValidationResults.Any())
                return;

            db.ValidationResults.AddRange(
                new ValidationResult
                {
                    NationalCode = "0012345678",
                    IsValid = true,
                    Reason = "Validation Success",
                    CheckedAt = DateTime.Now,
                },
                new ValidationResult
                {
                    NationalCode = "0098765432",
                    IsValid = true,
                    Reason = "Validation Success",
                    CheckedAt = DateTime.Now,
                }
            );

            db.SaveChanges();
        }
    }

}
