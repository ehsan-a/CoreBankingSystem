using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Responses.Customer
{
    public class CustomerResponseDto
    {
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
