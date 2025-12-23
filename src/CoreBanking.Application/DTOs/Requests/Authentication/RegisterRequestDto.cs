using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreBanking.Application.DTOs.Requests.Authentication
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public Guid CustomerId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
