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
    public class MedicationsController : ControllerBase
    {
        /*
        * ----------------------------------------------------------------------
        * ---------------------------------GET----------------------------------
        * ----------------------------------------------------------------------
        */
        //Get all items
        [HttpGet]
        [Route("list/{Key}")]
        public List<Medications> GetAllMedicationsList(int key)
        {
            pethouseContext db = new pethouseContext();
            var mediacations = (from p in db.Medications
                            where p.PetId == key
                            orderby p.MedDate descending
                            select p).ToList();
            return mediacations;

        }
        //Get int-of items
        [HttpGet]
        [Route("count/{Key}")]
        public int GetAllMedications(int key)
        {
            pethouseContext db = new pethouseContext();
            var medications = (from p in db.Medications
                            where p.PetId == key
                            orderby p.MedDate descending
                            select p).Count();
            if (medications != 0)
            {
                return medications;
            }
            else
            {
                return 0;
            }

        }
        //Get latest item
        [HttpGet]
        [Route("{Key}")]
        public Medications GetTheLatestMedication(int key)
        {
            pethouseContext db = new pethouseContext();
            Medications medications = (from p in db.Medications
                                 where p.PetId == key
                                 orderby p.MedDate descending
                                 select p).FirstOrDefault();
            if (medications != null)
            {
                return medications;
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
        public ActionResult PostCreateNew([FromBody] Medications med)
        {
            pethouseContext db = new pethouseContext(); //Tietokanta yhteytden muodostus
            try
            {
                db.Medications.Add(med);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                string virhe = ex.GetType().Name.ToString() + ": " + ex.Message.ToString();
                return BadRequest("Request failed with error:\n" + virhe);
            }
            db.Dispose(); //Tietokannan vapautus
            return Ok(med.PetId); //Palauttaa vastaluodun uuden objektin avainarvon

        }
        /*
        * ----------------------------------------------------------------------
        * ---------------------------------PUT----------------------------------
        * ----------------------------------------------------------------------
        */
        [HttpPut]//<-- Filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{Key}")] //<--key == petId
        public ActionResult PutEdit(int key, [FromBody] Medications med)
        {
            pethouseContext db = new pethouseContext();
            try
            {
                Medications medDb = db.Medications.Find(key);
                if (med != null)
                {
                    medDb.Medname = med.Medname;
                    medDb.MedDate = med.MedDate;
                    medDb.MedExpDate = med.MedExpDate;
                    db.SaveChanges();

                    return Ok(medDb.MedId);
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
        public ActionResult DeleteAllMedications(int? key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            pethouseContext db = new pethouseContext();
            try
            {
                Medications medicationsRow = db.Medications.Where(s => s.PetId == key).FirstOrDefault();
                db.Remove(medicationsRow);
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
        public ActionResult DeleteMedicine(int? key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            pethouseContext db = new pethouseContext();
            try
            {
                Medications medications = db.Medications.Find(key);
                db.Remove(medications);
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
