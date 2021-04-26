using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pethouse_api.Models
{
    public partial class Breeds
    {
        public Breeds()
        {
            Pets = new HashSet<Pets>();
        }

        public int BreedId { get; set; }
        public string Breedname { get; set; }
        public int RaceId { get; set; }

        public virtual Races Race { get; set; }
        public virtual ICollection<Pets> Pets { get; set; }
    }
}
