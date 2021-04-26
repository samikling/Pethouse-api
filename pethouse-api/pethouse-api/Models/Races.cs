using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pethouse_api.Models
{
    public partial class Races
    {
        public Races()
        {
            Breeds = new HashSet<Breeds>();
            Pets = new HashSet<Pets>();
        }

        public int RaceId { get; set; }
        public string Racename { get; set; }

        public virtual ICollection<Breeds> Breeds { get; set; }
        public virtual ICollection<Pets> Pets { get; set; }
    }
}
