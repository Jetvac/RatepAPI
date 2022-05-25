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
        public int? ManufactoryId { get; set; }

        public string SerialNumber
        {
            get
            {
                return $"{Seria} {Number}";
            }
        }

        public virtual User Account { get; set; }
        public virtual Manufactory? Manufactory { get; set; }
        public virtual PassportDatum PassportDatum { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
