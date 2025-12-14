using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreBanking.Application.DTOs
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public Guid CustomerId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
