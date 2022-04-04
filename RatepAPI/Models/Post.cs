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

        public int CodePost { get; set; }
        public string NamePost { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
