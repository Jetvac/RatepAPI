using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Manufactory
    {
        public Manufactory()
        {
            Employees = new HashSet<Employee>();
            PartAssemblyUnits = new HashSet<PartAssemblyUnit>();
        }

        public int ManufactoryId { get; set; }
        public int ManufactoryTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ManufactoryType ManufactoryType { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PartAssemblyUnit> PartAssemblyUnits { get; set; }
    }
}
