using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Internals
{
    public class RefreshTokenDto
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }
        public string? RevokedReason { get; set; }

        public bool IsExpired => DateTime.Now >= ExpiresAt;
        public bool IsRevoked => RevokedAt != null;
    }
}
