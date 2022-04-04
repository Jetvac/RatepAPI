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

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Manufactory> Manufactories { get; set; }
    }
}
