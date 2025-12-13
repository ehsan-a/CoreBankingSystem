using Microsoft.EntityFrameworkCore;
using PoliceClearanceService.Models;
using System;

namespace PoliceClearanceService.Data
{
    public class PoliceClearanceDbContext : DbContext
    {
        public PoliceClearanceDbContext(DbContextOptions<PoliceClearanceDbContext> options) : base(options)
        {
        }

        protected PoliceClearanceDbContext()
        {
        }
        public DbSet<PoliceClearanceResult> PoliceClearanceResults => Set<PoliceClearanceResult>();
    }
}
