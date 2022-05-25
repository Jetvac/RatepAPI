using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class PartAssemblyUnitViewFa
    {
        public string Articul { get; set; } = null!;
        public int CostId { get; set; }
        public int ManufactoryId { get; set; }
        public string Name { get; set; } = null!;
    }
}
