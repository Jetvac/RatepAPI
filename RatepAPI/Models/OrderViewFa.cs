using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class OrderViewFa
    {
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public int Vatid { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public decimal Cost { get; set; }
        public double Discount { get; set; }
    }
}
