using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class OrderPosition
    {
        public int PosId { get; set; }
        public int OrderId { get; set; }
        public int Pauid { get; set; }
        public int Ammount { get; set; }
        public DateTime? CompletionDate { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual PartAssemblyUnit Pau { get; set; } = null!;
    }
}
