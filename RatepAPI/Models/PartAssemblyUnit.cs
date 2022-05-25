using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class PartAssemblyUnit
    {
        public PartAssemblyUnit()
        {
            OrderPositions = new HashSet<OrderPosition>();
            RoadMapPauidcontainsNavigations = new HashSet<RoadMap>();
            RoadMapPaus = new HashSet<RoadMap>();
            StructurePauidcontainsNavigations = new HashSet<Structure>();
            StructurePaus = new HashSet<Structure>();
        }

        public string Articul { get; set; } = null!;
        public int CostId { get; set; }
        public int ManufactoryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual CostJournal Cost { get; set; } = null!;
        public virtual Manufactory Manufactory { get; set; } = null!;
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
        public virtual ICollection<RoadMap> RoadMapPauidcontainsNavigations { get; set; }
        public virtual ICollection<RoadMap> RoadMapPaus { get; set; }
        public virtual ICollection<Structure> StructurePauidcontainsNavigations { get; set; }
        public virtual ICollection<Structure> StructurePaus { get; set; }
    }
}
