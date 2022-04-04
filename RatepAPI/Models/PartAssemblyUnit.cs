using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class PartAssemblyUnit
    {
        public PartAssemblyUnit()
        {
            OrderPositions = new HashSet<OrderPosition>();
            StructureNumProductWhatNavigations = new HashSet<Structure>();
            StructureNumProductWhereNavigations = new HashSet<Structure>();
        }

        public int Pauid { get; set; }
        public int ManufactoryId { get; set; }
        public string NameProduct { get; set; } = null!;

        public virtual Manufactory Manufactory { get; set; } = null!;
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
        public virtual ICollection<Structure> StructureNumProductWhatNavigations { get; set; }
        public virtual ICollection<Structure> StructureNumProductWhereNavigations { get; set; }
    }
}
