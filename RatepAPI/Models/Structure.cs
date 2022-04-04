using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Structure
    {
        public int NumProductWhat { get; set; }
        public int NumProductWhere { get; set; }
        public int Quantity { get; set; }

        public virtual PartAssemblyUnit NumProductWhatNavigation { get; set; } = null!;
        public virtual PartAssemblyUnit NumProductWhereNavigation { get; set; } = null!;
    }
}
