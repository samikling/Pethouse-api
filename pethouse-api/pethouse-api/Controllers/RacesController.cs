using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pethouse_api.Models;
namespace pethouse_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        [HttpGet]
        public List<Races> GetAllRaces()
        {
            pethouseContext context = new pethouseContext();
            List<Races> races = context.Races.ToList();
            return races;
        }
    }
}
