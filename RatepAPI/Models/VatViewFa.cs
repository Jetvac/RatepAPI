using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class VatViewFa
    {
        public int Vatid { get; set; }
        public string Name { get; set; } = null!;
        public double Value { get; set; }
    }
}
