using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class OrderPositionViewFa
    {
        public int PosId { get; set; }
        public int OrderId { get; set; }
        public string Articul { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
