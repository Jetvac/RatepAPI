using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class CostJournalViewFa
    {
        public int CostId { get; set; }
        public decimal Value { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
