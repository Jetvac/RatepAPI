using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class CostJournal
    {
        public CostJournal()
        {
            PartAssemblyUnits = new HashSet<PartAssemblyUnit>();
        }

        public int CostId { get; set; }
        public decimal Value { get; set; }
        public DateTime ChangeDate { get; set; }

        public virtual ICollection<PartAssemblyUnit> PartAssemblyUnits { get; set; }
    }
}
