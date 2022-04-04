using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class CardWork
    {
        public int Pauid { get; set; }
        public int? CodePost { get; set; }
        public int CodeOperation { get; set; }
        public int OperatonOrder { get; set; }
        public int ElapsedTime { get; set; }

        public virtual Operation CodeOperationNavigation { get; set; } = null!;
        public virtual Post? CodePostNavigation { get; set; }
        public virtual PartAssemblyUnit Pau { get; set; } = null!;
    }
}
