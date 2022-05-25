using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class ManufactoryViewFa
    {
        public int ManufactoryId { get; set; }
        public int ManufactoryTypeId { get; set; }
        public string Name { get; set; } = null!;
    }
}
