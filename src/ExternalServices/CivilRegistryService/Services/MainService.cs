using CivilRegistryService.Data;
using CivilRegistryService.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CivilRegistryService.Services
{
    public class MainService
    {
        private readonly CivilRegistryDbContext _context;

        public MainService(CivilRegistryDbContext context)
        {
            _context = context;
        }

        public async Task<Person?> GetPersonAsync(string nationalCode)
        => await _context.People.FirstOrDefaultAsync(x => x.NationalCode == nationalCode);

    }
}
