using CivilRegistryService.Models;

namespace CivilRegistryService.Data
{
    public static class SeedData
    {
        public static void Seed(CivilRegistryDbContext db)
        {
            if (db.People.Any())
                return;

            db.People.AddRange(
                new Person
                {
                    NationalCode = "0012345678",
                    FirstName = "Ali",
                    LastName = "Ahmadi",
                    BirthDate = new DateTime(1990, 1, 1),
                    IsAlive = true
                },
                new Person
                {
                    NationalCode = "0098765432",
                    FirstName = "Reza",
                    LastName = "Mohammadi",
                    BirthDate = new DateTime(1985, 6, 15),
                    IsAlive = true
                }
            );

            db.SaveChanges();
        }
    }

}
