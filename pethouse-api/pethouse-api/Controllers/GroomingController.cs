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
        /*
       * ----------------------------------------------------------------------
       * ---------------------------------GET----------------------------------
       * ----------------------------------------------------------------------
       */
        //Get Int-of items
        [HttpGet]
        [Route("count/{Key}")]
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
        //Get latest item
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

        //Get List of items
        [HttpGet]
        [Route("list/{Key}")]
        public List<Grooming> GetAllGroomingList(int key)
        {
            pethouseContext db = new pethouseContext();
            var grooming = (from p in db.Grooming
                            where p.PetId == key
                            orderby p.GroomDate descending
                            select p).ToList();
            return grooming;

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
        * ---------------------------------PUT----------------------------------
        * ----------------------------------------------------------------------
        */
        [HttpPut]//<-- Filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{Key}")] //<--key == petId
        public ActionResult PutEdit(int key, [FromBody] Grooming gro)
        {
            pethouseContext db = new pethouseContext();
            try
            {
                Grooming groDb = db.Grooming.Find(key);
                if (gro != null)
                {
                    groDb.Groomname = gro.Groomname;
                    groDb.GroomDate = gro.GroomDate;
                    groDb.GroomExpDate = gro.GroomExpDate;
                    groDb.Comments = gro.Comments;
                    db.SaveChanges();

                    return Ok(groDb.GroomId);
                }
                else
                {
                    return NotFound("Not found");
                }
            }
            catch (Exception)
            {

                return BadRequest("Error");
            }
            finally
            {
                db.Dispose();
            }
        }
        /*
        * ----------------------------------------------------------------------
        * ---------------------------------DELETE-------------------------------
        * ----------------------------------------------------------------------
        */
        [HttpDelete]
        [Route("list/{key}")]
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
        /*
       * ----------------------------------------------------------------------
       * ---------------------------------DELETE-------------------------------
       * ----------------------------------------------------------------------
       */
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteGrooming(int? key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            pethouseContext db = new pethouseContext();
            try
            {
                Grooming grooming = db.Grooming.Find(key);
                db.Remove(grooming);
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


