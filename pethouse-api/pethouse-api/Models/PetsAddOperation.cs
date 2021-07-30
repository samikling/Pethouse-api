using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pethouse_api.Models
{
    public class PetsAddOperation
    {
        public int PetId { get; set; }
        public string Petname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Photo { get; set; }
        public int UserId { get; set; }
        public int? RaceId { get; set; }
        public int? BreedId { get; set; }
    }
}
