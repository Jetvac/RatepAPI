using System;
using System.Collections.Generic;

namespace RatepAPI.Models
{
    public partial class Gender
    {
        public Gender()
        {
            NaturalPeople = new HashSet<NaturalPerson>();
            PassportData = new HashSet<PassportDatum>();
        }

        public int GenderId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<NaturalPerson> NaturalPeople { get; set; }
        public virtual ICollection<PassportDatum> PassportData { get; set; }
    }
}
