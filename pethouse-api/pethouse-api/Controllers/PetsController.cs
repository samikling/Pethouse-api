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
        /*
         * ----------------------------------------------------------------------
         * ---------------------------------GET----------------------------------
         * ----------------------------------------------------------------------
         */
    public class PetsController : ControllerBase
    {
        [HttpGet]
        [Route("{key}")]
        //Hae yksi lemmikki lemmikki id:n perusteella
        public Pets GetOnePetbyId(int key)
        {
            pethouseContext db = new pethouseContext();
            Pets pet = db.Find<Pets>(key);
            return pet;
        }
        [HttpGet]
        [Route("user/{key}")]
        //Hae kaikki lemmikit käyttäjä id:n perusteella
        public List<Pets> GetAllPetsByUser(int key)
        {
            pethouseContext db = new pethouseContext();
            List<Pets> pets = (from p in db.Pets
                               where p.UserId == key
                               select p).ToList();
            return pets;
        }
        
        /*
         * ----------------------------------------------------------------------
         * ---------------------------------POST---------------------------------
         * ----------------------------------------------------------------------
         */
        
        [HttpPost] //<-- filtteri, joka sallii vain POST-metodit
        [Route("user/")]// <-- Routen placeholder
        public ActionResult PostCreateNew([FromBody] Pets pet)
        {
            pethouseContext db = new pethouseContext(); //Tietokanta yhteytden muodostus
            try
            {
                db.Pets.Add(pet);
                db.SaveChanges();

            }
            catch (Exception)
            {

                return BadRequest("Jokin meni pieleen asiakasta lisättäessä.\nOta yhteyttä Guruun!");
            }
            db.Dispose(); //Tietokannan vapautus
            return Ok(pet.PetId); //Palauttaa vastaluodun uuden lemmikin avainarvon

        }
        /*
         * ----------------------------------------------------------------------
         * ---------------------------------PUT----------------------------------
         * ----------------------------------------------------------------------
         */
        [HttpPut]//<-- Filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<--key == petId
        public ActionResult PutEdit(int key, [FromBody] Pets pet)
        {
            pethouseContext db = new pethouseContext();
            try
            {
                Pets petDb = db.Pets.Find(key);
                if (pet != null)
                {
                    petDb.Petname = pet.Petname;
                    petDb.Birthdate = pet.Birthdate;
                    petDb.Photo = pet.Photo;
                    petDb.User = pet.User;
                    petDb.RaceId = pet.RaceId;
                    petDb.BreedId = pet.BreedId;
                    db.SaveChanges();

                    return Ok(petDb.PetId);
                }
                else
                {
                    return NotFound("No such ID in pets");
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
        public ActionResult DeleteOnePet(int? key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            pethouseContext db = new pethouseContext();
            Pets pet = db.Pets.Find(key);
            try
            {
                if (pet != null)
                {
                    db.Pets.Remove(pet);
                    db.SaveChanges();
                    return Ok("Pet " + key + " removed");
                }
                else
                {
                    return NotFound("Pet " + key + " not found.");
                }
            }catch (Exception ex) {  return BadRequest(ex.ToString()); }
        }

    } 
}
