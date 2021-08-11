using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using pethouse_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pethouse_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        [HttpGet]
        [Route("{Key}")]
        //Hae kaikki Lääkitykset
        public Medications GetAllMedications(int key)
        {
            pethouseContext db = new pethouseContext();
            Medications medications = (from p in db.Medications
                                           where p.PetId == key
                                           select p).FirstOrDefault();
            if (medications != null)
            {
                return medications;
            }
            else
            {
                return null;
            }
            
        }
    }
}
