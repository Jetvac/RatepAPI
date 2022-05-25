using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class RoadMapViewFa
    {
        public int OrderId { get; set; }
        public int PosId { get; set; }
        public string Pauid { get; set; } = null!;
        public string Pauidcontains { get; set; } = null!;
        public int RoadMapStatusId { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int Quantity { get; set; }
    }
}
