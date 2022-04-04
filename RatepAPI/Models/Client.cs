using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Client
    {
        public Client()
        {
            Contracts = new HashSet<Contract>();
        }

        public int ClientId { get; set; }
        public int AccountId { get; set; }

        public virtual User Account { get; set; } = null!;
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
