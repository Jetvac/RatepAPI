using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Unit
    {
        public Unit()
        {
            Materials = new HashSet<Material>();
        }

        public int NumUnit { get; set; }
        public string NameUnit { get; set; } = null!;

        public virtual ICollection<Material> Materials { get; set; }
    }
}
