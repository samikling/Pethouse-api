using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pethouse_api.Models
{
    public partial class Grooming
    {
        [Key]
        public int GroomId { get; set; }
        public string Groomname { get; set; }
        public DateTime? GroomDate { get; set; }
        public DateTime? GroomExpDate { get; set; }
        public string Comments { get; set; }
        public int PetId { get; set; }

        public virtual Pets Pet { get; set; }
    }
}
