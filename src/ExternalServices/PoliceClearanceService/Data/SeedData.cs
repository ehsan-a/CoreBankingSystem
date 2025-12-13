using PoliceClearanceService.Models;
using System;

namespace PoliceClearanceService.Data
{
    public static class SeedData
    {
        public static void Seed(PoliceClearanceDbContext db)
        {
            if (db.PoliceClearanceResults.Any())
                return;

            db.PoliceClearanceResults.AddRange(
                new Models.PoliceClearanceResult
                {
                    NationalCode = "0012345678",
                    HasCriminalRecord = false,
                    Description = "No Criminal Record",
                    CheckedAt = DateTime.Now,

                },
                new PoliceClearanceResult
                {
                    NationalCode = "0098765432",
                    HasCriminalRecord = false,
                    Description = "No Criminal Record",
                    CheckedAt = DateTime.Now,
                }
            );

            db.SaveChanges();
        }
    }
}
