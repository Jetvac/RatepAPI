using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Vat
    {
        public Vat()
        {
            Orders = new HashSet<Order>();
        }

        public int Vatid { get; set; }
        public string Name { get; set; } = null!;
        public double Value { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
