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
        [HttpGet]
        [Route("")]
        //Hae kaikki lemmikit
        public List<Pets> GetAllPets()
        {
            pethouseContext db = new pethouseContext();
            List<Pets> pets = db.Pets.ToList();
            return pets;
        }
        // GET: api/<PetsController>
        //Testataan erillaisia get vaihtoehtoja
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
        [HttpPut]//<-- Filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<--key == petId
        public ActionResult PutEdit(string key, [FromBody] Pets pet)
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
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteOnePet(string key)
        {
            pethouseContext db = new pethouseContext();
            Pets pet = db.Pets.Find(key);
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
        }

    } 
}
