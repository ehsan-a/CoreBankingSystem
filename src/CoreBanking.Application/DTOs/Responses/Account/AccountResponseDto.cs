using CoreBanking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Responses.Account
{
    public class AccountResponseDto
    {
        public string AccountNumber { get; set; } = default!;

        public decimal Balance { get; set; }

        public AccountStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CustomerId { get; set; }
    }
}
