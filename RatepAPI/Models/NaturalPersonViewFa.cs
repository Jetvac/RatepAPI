using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class NaturalPersonViewFa
    {
        public int NaturalPersonId { get; set; }
        public int ClientId { get; set; }
        public int GenderId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
    }
}
