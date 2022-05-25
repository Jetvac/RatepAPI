using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Client
    {
        public Client()
        {
            LegalPeople = new HashSet<LegalPerson>();
            NaturalPeople = new HashSet<NaturalPerson>();
            Orders = new HashSet<Order>();
        }

        public int ClientId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<LegalPerson> LegalPeople { get; set; }
        public virtual ICollection<NaturalPerson> NaturalPeople { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
