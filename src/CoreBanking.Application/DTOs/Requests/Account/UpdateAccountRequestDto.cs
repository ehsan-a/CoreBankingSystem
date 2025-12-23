using CoreBanking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Requests.Account
{
    public class UpdateAccountRequestDto
    {
        public Guid Id { get; set; }
        public AccountStatus Status { get; set; }
    }
}
