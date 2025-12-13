using Microsoft.EntityFrameworkCore;
using PoliceClearanceService.Data;
using PoliceClearanceService.Models;
using System;

namespace PoliceClearanceService.Services
{
    public class MainService
    {
        private readonly PoliceClearanceDbContext _context;

        public MainService(PoliceClearanceDbContext context)
        {
            _context = context;
        }

        public async Task<PoliceClearanceResult?> GetResultAsync(string nationalCode)
        => await _context.PoliceClearanceResults.FirstOrDefaultAsync(x => x.NationalCode == nationalCode);

    }
}
