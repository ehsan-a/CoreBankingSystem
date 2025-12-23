using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Requests.Customer
{
    public class UpdateCustomerRequestDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
