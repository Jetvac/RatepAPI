using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class UserViewFa
    {
        public int AccountId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
