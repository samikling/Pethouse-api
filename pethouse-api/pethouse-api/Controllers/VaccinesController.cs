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
        /*
        * ----------------------------------------------------------------------
        * ---------------------------------GET----------------------------------
        * ----------------------------------------------------------------------
        */

        //Get Int-of items
        [HttpGet]
        [Route("count/{Key}")]
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
        
        //Get list of items

        [HttpGet]
        [Route("list/{Key}")]
        //Hae kaikki Rokotukset
        public List<Vaccines> GetAllVaccinesList(int key)
        {
            pethouseContext db = new pethouseContext();
            var vaccines = (from p in db.Vaccines
                            where p.PetId == key
                            orderby p.VacDate descending
                            select p).ToList();
            return vaccines;

        }
        //Get latest item
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
        * ---------------------------------PUT----------------------------------
        * ----------------------------------------------------------------------
        */
        [HttpPut]//<-- Filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{Key}")] //<--key == petId
        public ActionResult PutEdit(int key,[FromBody] Vaccines vac)
        {
            pethouseContext db = new pethouseContext();
            try
            {
                Vaccines vacDb = db.Vaccines.Find(key);
                if (vac != null)
                {
                    vacDb.Vacname = vac.Vacname;
                    vacDb.VacDate = vac.VacDate;
                    vacDb.VacExpDate = vac.VacExpDate;
                    db.SaveChanges();

                    return Ok(vacDb.VacId);
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
        [Route("{key}")]
        public ActionResult DeleteVaccine(int? key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            pethouseContext db = new pethouseContext();
            try
            { Vaccines vaccine = db.Vaccines.Find(key);
                db.Remove(vaccine);
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
        [Route("list/{key}")]
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

