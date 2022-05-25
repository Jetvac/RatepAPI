using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class LegalPersonViewFa
    {
        public int LegalPersonId { get; set; }
        public int ClientId { get; set; }
        public string Tin { get; set; } = null!;
        public string Bic { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
