using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.Persistence.Entities
{
    public class IdempotencyKey
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Key { get; set; }
        public string Result { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
