using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Post
    {
        public Post()
        {
            Employees = new HashSet<Employee>();
        }

        public int PostId { get; set; }
        public int? RoleId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Role? Role { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
