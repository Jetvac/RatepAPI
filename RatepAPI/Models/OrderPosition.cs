using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class OrderPosition
    {
        public OrderPosition()
        {
            RoadMaps = new HashSet<RoadMap>();
        }

        public int PosId { get; set; }
        public int OrderId { get; set; }
        public string Articul { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual PartAssemblyUnit ArticulNavigation { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual ICollection<RoadMap> RoadMaps { get; set; }
    }
}
