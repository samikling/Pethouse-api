using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pethouse_api.Models;
using System.Collections;
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

        [HttpGet]
        [Route("user/dogs/{key}")]
        [Produces("application/json")]
        public string[] GetDogsByUser(int key)
        {
            pethouseContext db = new pethouseContext();
            var userPets = (from p in db.Pets
                       where p.UserId == key && p.RaceId == 1
                       select p.Petname).ToArray();
            return userPets;
            
        }
        [HttpGet]
        [Route("user/cats/{key}")]
        [Produces("application/json")]
        public string[] GetCatsByUser(int key)
        {
            pethouseContext db = new pethouseContext();
            var userPets = (from p in db.Pets
                            where p.UserId == key && p.RaceId == 2
                            select p.Petname).ToArray();
            return userPets;

        }
        [HttpGet]
        [Route("user/test/{key}")]
        [Produces("application/json")]
        public IEnumerable<Pets> GetByUserTest(int key)
        {
            pethouseContext db = new pethouseContext();
            var userPets = (from p in db.Pets
                            where p.UserId == key && p.RaceId == 2
                            select p);
            return userPets;

        }
    } 
}
