using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderPositions = new HashSet<OrderPosition>();
            RoadMaps = new HashSet<RoadMap>();
        }

        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public int Vatid { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public decimal Cost { get; set; }
        public double Discount { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual OrderStatus OrderStatus { get; set; } = null!;
        public virtual Vat Vat { get; set; } = null!;
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
        public virtual ICollection<RoadMap> RoadMaps { get; set; }
    }
}
