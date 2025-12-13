using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.ExternalServices.CivilRegistry
{
    public class CivilRegistryDto
    {
        public int Id { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAlive { get; set; }
    }
}
