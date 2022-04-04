using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Contracts = new HashSet<Contract>();
        }

        public int EmployeeId { get; set; }
        public int AccountId { get; set; }
        public int CodePost { get; set; }
        public int? ManufactoryId { get; set; }

        public virtual User Account { get; set; } = null!;
        public virtual Post CodePostNavigation { get; set; } = null!;
        public virtual Manufactory? Manufactory { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
