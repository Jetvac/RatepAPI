using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Role
    {
        public Role()
        {
            Posts = new HashSet<Post>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
    }
}
