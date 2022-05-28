using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class EmployeeViewFa
    {
        public int EmployeeId { get; set; }
        public string Seria { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int PostId { get; set; }
        public int AccountId { get; set; }
        public string PhoneNumber { get; set; } = null!;
    }
}
