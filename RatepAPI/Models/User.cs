using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class User
    {
        public User()
        {
            Employees = new HashSet<Employee>();
        }

        public int AccountId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
