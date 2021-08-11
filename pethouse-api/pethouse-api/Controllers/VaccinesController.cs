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
    public class VaccinesController : ControllerBase
    {
        [HttpGet]
        [Route("{Key}")]
        //Hae kaikki Rokotukset
        public Vaccines GetAllVaccines(int key)
        {
            pethouseContext db = new pethouseContext();
            Vaccines vaccines =       (from p in db.Vaccines
                                           where p.PetId == key
                                           select p).FirstOrDefault();
            if (vaccines != null)
            {
                return vaccines;
            }
            else
            {
                return null;
            }
            
        }
    }
}
