using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pethouse_api.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pethouse_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        // GET: api/<PetsController>
        [HttpGet]
        public string[] GetAll()
        {
            string[] petNames = null;
            pethouseContext context = new pethouseContext();

            petNames = (from e in context.Pets
                        where (e.UserId == 1)
                        select e.Petname + "-" +
                        e.Race.Racename + "-" + e.Breed.Breedname).ToArray();

            return petNames;

        }
    } 
}
