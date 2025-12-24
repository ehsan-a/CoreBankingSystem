using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.Audit
{
    public class AuditLogService : IAuditLogService
    {
        private readonly CoreBankingContext _context;

        public AuditLogService(CoreBankingContext context)
        {
            _context = context;
        }

        public async Task LogAsync(AuditLog auditLog, CancellationToken cancellationToken = default)
        {
            await _context.AuditLogs.AddAsync(auditLog, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
