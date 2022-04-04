using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Material
    {
        public int CodeMaterial { get; set; }
        public string Name { get; set; } = null!;
        public int PriceMaterial { get; set; }
        public int NumUnit { get; set; }

        public virtual Unit NumUnitNavigation { get; set; } = null!;
    }
}
