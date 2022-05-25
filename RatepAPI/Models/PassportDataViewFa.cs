using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class PassportDataViewFa
    {
        public string Seria { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int GenderId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; } = null!;
    }
}
