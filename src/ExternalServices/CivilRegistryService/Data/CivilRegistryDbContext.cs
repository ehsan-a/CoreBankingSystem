using CivilRegistryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CivilRegistryService.Data
{
    public class CivilRegistryDbContext : DbContext
    {
        public CivilRegistryDbContext(DbContextOptions<CivilRegistryDbContext> options) : base(options)
        {
        }

        protected CivilRegistryDbContext()
        {
        }
        public DbSet<Person> People => Set<Person>();
    }
}
