using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pethouse_api.Models;

namespace pethouse_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedsController : ControllerBase
    {
        [HttpGet]
        public List<Breeds> GetAllBreeds()
        {
            pethouseContext context = new pethouseContext();
            List<Breeds> breeds = context.Breeds.ToList();
            return breeds;
        }
        
        
        [HttpGet]
        [Route("{key}")]
        //Hae yksi rotu
        public Breeds GetOneBreed(int key)
        {
            pethouseContext db = new pethouseContext();
            Breeds breed = db.Find<Breeds>(key);
            return breed;
        }
    }
}
