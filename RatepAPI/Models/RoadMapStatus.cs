using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class RoadMapStatus
    {
        public RoadMapStatus()
        {
            RoadMaps = new HashSet<RoadMap>();
        }

        public int RoadMapStatusId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<RoadMap> RoadMaps { get; set; }
    }
}
