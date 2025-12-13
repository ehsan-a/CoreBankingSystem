using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.ExternalServices.PoliceClearance
{
    public class PoliceClearanceDto
    {
        public int Id { get; set; }
        public string NationalCode { get; set; } = default!;

        public bool HasCriminalRecord { get; set; }

        public string? Description { get; set; }

        public DateTime CheckedAt { get; set; }
    }
}
