using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class PassportDatum
    {
        public PassportDatum()
        {
            Employees = new HashSet<Employee>();
        }

        public string Seria { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int GenderId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; } = null!;

        public virtual Gender Gender { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
