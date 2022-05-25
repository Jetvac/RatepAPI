using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class ClientViewFa
    {
        public int ClientId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
