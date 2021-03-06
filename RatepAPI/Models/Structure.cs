using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Structure
    {
        public string Pauid { get; set; } = null!;
        public string Pauidcontains { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual PartAssemblyUnit Pau { get; set; } = null!;
        public virtual PartAssemblyUnit PauidcontainsNavigation { get; set; } = null!;
    }
}
