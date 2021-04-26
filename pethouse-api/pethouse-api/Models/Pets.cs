using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pethouse_api.Models
{
    public partial class Pets
    {
        public int PetId { get; set; }
        public string Petname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Photo { get; set; }
        public int UserId { get; set; }
        public int? RaceId { get; set; }
        public int? BreedId { get; set; }

        public virtual Breeds Breed { get; set; }
        public virtual Races Race { get; set; }
        public virtual Users User { get; set; }
    }
}
