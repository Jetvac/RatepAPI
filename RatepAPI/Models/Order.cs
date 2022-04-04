using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderPositions = new HashSet<OrderPosition>();
        }

        public int OrderId { get; set; }
        public int ContractId { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        public virtual Contract Contract { get; set; } = null!;
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
    }
}
