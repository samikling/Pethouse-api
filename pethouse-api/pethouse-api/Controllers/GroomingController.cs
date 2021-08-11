using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pethouse_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pethouse_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroomingController : ControllerBase
    {
        [HttpGet]
        [Route("{Key}")]
        //Hae kaikki Hoidot
        public Grooming GetAllGrooming(int key)
        {
            pethouseContext db = new pethouseContext();
            Grooming grooming = (from p in db.Grooming
                                 where p.PetId == key
                                 select p).FirstOrDefault();
            if (grooming != null)
            {
                return grooming;
            }
            else
            {
                return null;
            }
        }
    }
}


