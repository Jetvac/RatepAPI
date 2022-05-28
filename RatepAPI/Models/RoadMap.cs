using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class RoadMap
    {
        public int OrderId { get; set; }
        public int PosId { get; set; }
        public string Pauid { get; set; } = null!;
        public string Pauidcontains { get; set; } = null!;
        public int RoadMapStatusId { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int Quantity { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Order Order { get; set; } = null!;
        public virtual PartAssemblyUnit Pau { get; set; } = null!;
        public virtual PartAssemblyUnit PauidcontainsNavigation { get; set; } = null!;
        public virtual OrderPosition Pos { get; set; } = null!;
        public virtual RoadMapStatus RoadMapStatus { get; set; } = null!;
    }
}
