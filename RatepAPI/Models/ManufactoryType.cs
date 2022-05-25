using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class ManufactoryType
    {
        public ManufactoryType()
        {
            Manufactories = new HashSet<Manufactory>();
        }

        public int ManufactoryTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Manufactory> Manufactories { get; set; }
    }
}
