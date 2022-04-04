using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class MaterialCard
    {
        public int NumMaterial { get; set; }
        public int Pauid { get; set; }
        public int Quantity { get; set; }

        public virtual Material NumMaterialNavigation { get; set; } = null!;
        public virtual PartAssemblyUnit Pau { get; set; } = null!;
    }
}
