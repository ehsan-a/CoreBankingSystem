using CoreBanking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; } = default!;
        public Guid? EntityId { get; set; }
        public AuditActionType ActionType { get; set; }
        public string PerformedBy { get; set; } = default!;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }
}
