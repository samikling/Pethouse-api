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
        [Route("list/{Key}")]
        //Hae kaikki Rokotukset
        public int GetAllVaccines(int key)
        {
            pethouseContext db = new pethouseContext();
            var vaccines = (from p in db.Vaccines
                                 where p.PetId == key
                                 orderby p.VacDate descending
                                 select p).Count();
            if (vaccines != 0)
            {
                return vaccines;
            }
            else
            {
                return 0;
            }

        }
        [HttpGet]
        [Route("{Key}")]
        //Hae kaikki Rokotukset
        public Vaccines GetTheLatestVaccine(int key)
        {
            pethouseContext db = new pethouseContext();
            Vaccines vaccines = (from p in db.Vaccines
                                 where p.PetId == key
                                 orderby p.VacDate descending
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
        /*
        * ----------------------------------------------------------------------
        * ---------------------------------POST---------------------------------
        * ----------------------------------------------------------------------
        */

        [HttpPost] //<-- filtteri, joka sallii vain POST-metodit
        //[Route("")]// <-- Routen placeholder
        public ActionResult PostCreateNew([FromBody] Vaccines vac)
        {
            pethouseContext db = new pethouseContext(); //Tietokanta yhteytden muodostus
            try
            {
                db.Vaccines.Add(vac);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                string virhe = ex.GetType().Name.ToString() + ": " + ex.Message.ToString();
                return BadRequest("Jokin meni pieleen asiakasta lisättäessä.\nOta yhteyttä Guruun!\n" + virhe);
            }
            db.Dispose(); //Tietokannan vapautus
            return Ok(vac.PetId); //Palauttaa vastaluodun uuden objektin avainarvon

        }
        /*
        * ----------------------------------------------------------------------
        * ---------------------------------DELETE-------------------------------
        * ----------------------------------------------------------------------
        */
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteAllVaccines(int? key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            pethouseContext db = new pethouseContext();
            try
            {
              Vaccines vaccineRow = db.Vaccines.Where(s => s.PetId == key).FirstOrDefault();
                 db.Remove(vaccineRow);
                 db.SaveChanges();
            }
           catch(Exception ex)
            {
                 return  BadRequest(ex.Message);
            }
               return Ok();
        }
    }
}

