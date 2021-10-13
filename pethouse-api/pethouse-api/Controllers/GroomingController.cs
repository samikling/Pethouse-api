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
        [Route("list/{Key}")]
        //Get the ammount of grooming items
        public int GetAllGrooming(int key)
        {
            pethouseContext db = new pethouseContext();
            var grooming = (from p in db.Grooming
                               where p.PetId == key
                               orderby p.GroomDate descending
                               select p).Count();
            if (grooming != 0)
            {
                return grooming;
            }
            else
            {
                return 0;
            }

        }
        [HttpGet]
        [Route("{Key}")]
        //Get the latest grooming operation
        public Grooming GetTheLatestGrooming(int key)
        {
            pethouseContext db = new pethouseContext();
            Grooming grooming = (from p in db.Grooming
                                       where p.PetId == key
                                       orderby p.GroomDate descending
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
        /*
        * ----------------------------------------------------------------------
        * ---------------------------------POST---------------------------------
        * ----------------------------------------------------------------------
        */

        [HttpPost] //<-- filtteri, joka sallii vain POST-metodit
        //[Route("")]// <-- Routen placeholder
        public ActionResult PostCreateNew([FromBody] Grooming grooming)
        {
            pethouseContext db = new pethouseContext(); //Tietokanta yhteytden muodostus
            try
            {
                db.Grooming.Add(grooming);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                string virhe = ex.GetType().Name.ToString() + ": " + ex.Message.ToString();
                return BadRequest("Request failed with error:\n" + virhe);
            }
            db.Dispose(); //Tietokannan vapautus
            return Ok(grooming.PetId); //Palauttaa vastaluodun uuden objektin avainarvon

        }
        /*
        * ----------------------------------------------------------------------
        * ---------------------------------DELETE-------------------------------
        * ----------------------------------------------------------------------
        */
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteAllGroomings(int? key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            pethouseContext db = new pethouseContext();
            try
            {
                Grooming groomingRow = db.Grooming.Where(s => s.PetId == key).FirstOrDefault();
                db.Remove(groomingRow);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}


