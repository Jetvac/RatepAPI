using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class PostViewFa
    {
        public int PostId { get; set; }
        public int? RoleId { get; set; }
        public string Name { get; set; } = null!;
    }
}
